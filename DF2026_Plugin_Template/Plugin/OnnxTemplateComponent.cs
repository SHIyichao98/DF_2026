using Grasshopper.Kernel;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DF2026.PluginTemplate
{
    /// <summary>
    /// Stable inference code for a tabular regression model.
    /// Most students should edit PluginConfig.cs rather than this file.
    /// </summary>
    public sealed class OnnxTemplateComponent : GH_Component
    {
        private InferenceSession? _session;
        private string? _loadedModelPath;

        public OnnxTemplateComponent()
            : base(
                PluginConfig.ComponentName,
                PluginConfig.ComponentNickname,
                PluginConfig.ComponentDescription,
                PluginConfig.Category,
                PluginConfig.Subcategory)
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            foreach (PortDefinition input in PluginConfig.Inputs)
            {
                pManager.AddNumberParameter(
                    input.Name,
                    input.Nickname,
                    input.Description,
                    GH_ParamAccess.item);
            }

            if (PluginConfig.EnableModelPathInput)
            {
                pManager.AddTextParameter(
                    "Model Path",
                    "Path",
                    "Optional ONNX path. Leave empty to load the model beside this GHA.",
                    GH_ParamAccess.item,
                    string.Empty);
                pManager[PluginConfig.Inputs.Length].Optional = true;
            }
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            foreach (PortDefinition output in PluginConfig.Outputs)
            {
                pManager.AddNumberParameter(
                    output.Name,
                    output.Nickname,
                    output.Description,
                    GH_ParamAccess.item);
            }

            pManager.AddTextParameter(
                "Model Info",
                "Info",
                "Resolved model path and ONNX tensor metadata.",
                GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            if (PluginConfig.Inputs.Length == 0)
            {
                AddRuntimeMessage(
                    GH_RuntimeMessageLevel.Error,
                    "PluginConfig.Inputs must contain at least one input.");
                return;
            }

            if (PluginConfig.Outputs.Length == 0)
            {
                AddRuntimeMessage(
                    GH_RuntimeMessageLevel.Error,
                    "PluginConfig.Outputs must contain at least one output.");
                return;
            }

            var features = new float[PluginConfig.Inputs.Length];
            for (int i = 0; i < features.Length; i++)
            {
                double value = 0.0;
                if (!DA.GetData(i, ref value))
                {
                    return;
                }
                features[i] = (float)value;
            }

            string requestedPath = string.Empty;
            if (PluginConfig.EnableModelPathInput)
            {
                DA.GetData(PluginConfig.Inputs.Length, ref requestedPath);
            }

            try
            {
                string modelPath = ResolveModelPath(requestedPath);
                if (!File.Exists(modelPath))
                {
                    AddRuntimeMessage(
                        GH_RuntimeMessageLevel.Error,
                        $"ONNX model not found: {modelPath}");
                    return;
                }

                EnsureSession(modelPath);
                if (_session is null)
                {
                    AddRuntimeMessage(
                        GH_RuntimeMessageLevel.Error,
                        "The ONNX session could not be created.");
                    return;
                }

                string inputName = ResolveTensorName(
                    PluginConfig.InputTensorName,
                    _session.InputMetadata,
                    "input");
                string outputName = ResolveTensorName(
                    PluginConfig.OutputTensorName,
                    _session.OutputMetadata,
                    "output");

                NodeMetadata inputMetadata = _session.InputMetadata[inputName];
                if (inputMetadata.ElementType != typeof(float))
                {
                    AddRuntimeMessage(
                        GH_RuntimeMessageLevel.Error,
                        $"Input '{inputName}' uses {inputMetadata.ElementType.Name}; " +
                        "this template supports float32 inputs only.");
                    return;
                }

                if (inputMetadata.Dimensions.Length != 2)
                {
                    AddRuntimeMessage(
                        GH_RuntimeMessageLevel.Error,
                        $"Input '{inputName}' has rank {inputMetadata.Dimensions.Length}; " +
                        "this template expects shape [batch, features].");
                    return;
                }

                int declaredFeatureCount = inputMetadata.Dimensions.Last();
                if (declaredFeatureCount > 0 &&
                    declaredFeatureCount != PluginConfig.Inputs.Length)
                {
                    AddRuntimeMessage(
                        GH_RuntimeMessageLevel.Error,
                        $"PluginConfig defines {PluginConfig.Inputs.Length} inputs, " +
                        $"but ONNX expects {declaredFeatureCount} features.");
                    return;
                }

                var inputTensor = new DenseTensor<float>(
                    features,
                    new[] { 1, features.Length });
                var inputs = new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor(inputName, inputTensor)
                };

                using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results =
                    _session.Run(inputs);

                DisposableNamedOnnxValue selectedResult =
                    results.Single(result => result.Name == outputName);
                float[] outputValues =
                    selectedResult.AsTensor<float>().ToArray();

                if (outputValues.Length != PluginConfig.Outputs.Length)
                {
                    AddRuntimeMessage(
                        GH_RuntimeMessageLevel.Error,
                        $"PluginConfig defines {PluginConfig.Outputs.Length} outputs, " +
                        $"but ONNX returned {outputValues.Length} values.");
                    return;
                }

                for (int i = 0; i < outputValues.Length; i++)
                {
                    DA.SetData(i, outputValues[i]);
                }

                DA.SetDataList(
                    PluginConfig.Outputs.Length,
                    BuildModelInfo(modelPath));
            }
            catch (OnnxRuntimeException ex)
            {
                AddRuntimeMessage(
                    GH_RuntimeMessageLevel.Error,
                    $"ONNX Runtime: {ex.Message}");
            }
            catch (Exception ex)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, ex.Message);
            }
        }

        private static string ResolveTensorName(
            string configuredName,
            IReadOnlyDictionary<string, NodeMetadata> metadata,
            string tensorRole)
        {
            if (!string.IsNullOrWhiteSpace(configuredName))
            {
                if (!metadata.ContainsKey(configuredName))
                {
                    throw new InvalidOperationException(
                        $"Configured {tensorRole} tensor '{configuredName}' was not found. " +
                        $"Available names: {string.Join(", ", metadata.Keys)}");
                }
                return configuredName;
            }

            if (metadata.Count != 1)
            {
                throw new InvalidOperationException(
                    $"The model has {metadata.Count} {tensorRole} tensors. " +
                    $"Set the tensor name explicitly in PluginConfig.cs.");
            }

            return metadata.Keys.Single();
        }

        private static string ResolveModelPath(string requestedPath)
        {
            string assemblyDirectory =
                Path.GetDirectoryName(typeof(OnnxTemplateComponent).Assembly.Location)
                ?? AppContext.BaseDirectory;

            if (string.IsNullOrWhiteSpace(requestedPath))
            {
                return Path.Combine(
                    assemblyDirectory,
                    PluginConfig.ModelFileName);
            }

            string expandedPath =
                Environment.ExpandEnvironmentVariables(requestedPath.Trim());
            return Path.IsPathRooted(expandedPath)
                ? Path.GetFullPath(expandedPath)
                : Path.GetFullPath(Path.Combine(assemblyDirectory, expandedPath));
        }

        private void EnsureSession(string modelPath)
        {
            if (_session is not null &&
                string.Equals(
                    _loadedModelPath,
                    modelPath,
                    StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            _session?.Dispose();
            _session = new InferenceSession(modelPath);
            _loadedModelPath = modelPath;
        }

        private IEnumerable<string> BuildModelInfo(string modelPath)
        {
            if (_session is null)
            {
                yield break;
            }

            yield return $"Model: {modelPath}";
            foreach (KeyValuePair<string, NodeMetadata> input in _session.InputMetadata)
            {
                yield return
                    $"Input: {input.Key} | {input.Value.ElementType.Name} | " +
                    $"[{string.Join(", ", input.Value.Dimensions)}]";
            }
            foreach (KeyValuePair<string, NodeMetadata> output in _session.OutputMetadata)
            {
                yield return
                    $"Output: {output.Key} | {output.Value.ElementType.Name} | " +
                    $"[{string.Join(", ", output.Value.Dimensions)}]";
            }
        }

        public override void RemovedFromDocument(GH_Document document)
        {
            _session?.Dispose();
            _session = null;
            _loadedModelPath = null;
            base.RemovedFromDocument(document);
        }

        protected override System.Drawing.Bitmap Icon => IconLoader.ComponentIcon;

        public override Guid ComponentGuid => PluginConfig.ComponentId;
    }
}
