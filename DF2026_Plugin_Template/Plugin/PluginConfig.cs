using System;

namespace DF2026.PluginTemplate
{
    /// <summary>
    /// A Grasshopper port displayed on the component.
    /// </summary>
    public sealed class PortDefinition
    {
        public PortDefinition(string name, string nickname, string description)
        {
            Name = name;
            Nickname = nickname;
            Description = description;
        }

        public string Name { get; }

        public string Nickname { get; }

        public string Description { get; }
    }

    /// <summary>
    /// STUDENTS: This is the main file to edit.
    /// Search for "EDIT" and update each section for your model.
    /// </summary>
    public static class PluginConfig
    {
        // EDIT 1: Name the plugin and Grasshopper component.
        public const string PluginName = "DF2026 ONNX Example";
        public const string PluginDescription =
            "A configurable ONNX regression component created in the DF2026 workshop.";
        public const string ComponentName = "ONNX Model Predictor";
        public const string ComponentNickname = "ONNX Predict";
        public const string ComponentDescription =
            "Runs one float32 ONNX input tensor and returns one float32 output tensor.";
        public const string Category = "DF2026";
        public const string Subcategory = "ONNX Templates";
        public const string AuthorName = "Your Name";
        public const string AuthorContact = "your.email@example.com";

        // EDIT 2: Generate two new GUIDs for every new plugin.
        // In Visual Studio: Tools > Create GUID > Registry Format > Copy.
        // Keep these values unchanged after sharing Grasshopper files.
        public static readonly Guid AssemblyId =
            new Guid("287b82ac-1845-46ed-8465-89dc7615de2e");
        public static readonly Guid ComponentId =
            new Guid("e9236a90-c32f-40e9-85bd-a8fdd7164158");

        // EDIT 3: Replace Model/model.onnx, or change this filename.
        public const string ModelFileName = "model.onnx";

        // EDIT 4: Set the ONNX tensor names.
        // Use an empty string to auto-select when the model has exactly one
        // input or output tensor.
        public const string InputTensorName = "float_input";
        public const string OutputTensorName = "variable";

        // EDIT 5: Define the Grasshopper numeric inputs in the exact order used
        // during model training. The number of entries must match the ONNX
        // feature dimension.
        public static readonly PortDefinition[] Inputs =
        {
            new PortDefinition("X1", "X1", "Raw numeric feature X1."),
            new PortDefinition("X2", "X2", "Raw numeric feature X2."),
            new PortDefinition("X3", "X3", "Raw numeric feature X3."),
            new PortDefinition("X4", "X4", "Raw numeric feature X4."),
            new PortDefinition("X5", "X5", "Raw numeric feature X5."),
            new PortDefinition("X6", "X6", "Raw numeric feature X6.")
        };

        // EDIT 6: Define one Grasshopper output for each value returned by the
        // selected ONNX output tensor. The sample model returns one value.
        public static readonly PortDefinition[] Outputs =
        {
            new PortDefinition("Prediction", "Y", "Predicted target value.")
        };

        // Keep true during the workshop. This adds an optional text input that
        // lets you test a model outside the plugin folder.
        public const bool EnableModelPathInput = true;
    }
}
