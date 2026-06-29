# DF2026 Grasshopper ONNX Plugin Template

This template turns a tabular ONNX regression model into a compiled
Grasshopper plugin for Rhino 8.

It is designed for the DigitalFUTURES 2026 surrogate-model workshop. Students
should not need to rewrite the inference engine. In most cases, you only edit
one configuration file, replace one ONNX file, replace one image, and build.

## What this template supports

The template intentionally supports one clear model contract:

```text
Grasshopper numeric inputs
        |
        v
one float32 tensor with shape [1, number_of_features]
        |
        v
ONNX Runtime inference
        |
        v
one float32 output tensor
        |
        v
Grasshopper numeric outputs
```

Typical compatible models include:

- scikit-learn Random Forest regression exported with `skl2onnx`;
- Gradient Boosting regression exported with `skl2onnx`;
- a tabular ANN whose preprocessing is included in the exported model;
- another regression model with one float input tensor and one float output
  tensor.

This template does **not** automatically support image, text, graph, sequence,
multiple-input, or mixed-type ONNX models.

## Folder structure

```text
DF2026_Plugin_Template/
├─ README.md
├─ Assets/
│  └─ component_icon.png
├─ Model/
│  └─ model.onnx
└─ Plugin/
   ├─ DF2026.PluginTemplate.sln
   ├─ DF2026.PluginTemplate.csproj
   ├─ PluginConfig.cs
   ├─ OnnxTemplateComponent.cs
   ├─ PluginAssemblyInfo.cs
   ├─ IconLoader.cs
   └─ Properties/
      └─ launchSettings.json
```

## Files you should modify

For a normal workshop project, edit only:

1. `Plugin/PluginConfig.cs`
2. `Model/model.onnx`
3. `Assets/component_icon.png`

The stable inference implementation is in `OnnxTemplateComponent.cs`. You
usually do not need to edit it.

---

# Part 1 — Check your ONNX model

Before editing the plugin, identify:

- input tensor name;
- input tensor data type;
- input shape;
- number and order of features;
- output tensor name;
- number and order of output values;
- whether scaling or other preprocessing is already inside the model.

The template requires an input shape equivalent to:

```text
[batch, number_of_features]
```

with `float32` values.

In Python or Google Colab, inspect the model with:

```python
!pip install -q onnxruntime

import onnxruntime as ort

session = ort.InferenceSession("model.onnx")

for item in session.get_inputs():
    print("INPUT:", item.name, item.type, item.shape)

for item in session.get_outputs():
    print("OUTPUT:", item.name, item.type, item.shape)
```

The sample model included with the template reports approximately:

```text
INPUT:  float_input tensor(float) [None, 6]
OUTPUT: variable    tensor(float) [None, 1]
```

## Important: preprocessing

The Grasshopper values must be transformed exactly as they were during
training.

For example, if the Python model expects standardized values but the scaler is
not included in the ONNX graph, raw Grasshopper values will produce incorrect
predictions even though inference runs without an error.

Choose one of these approaches:

1. export the preprocessing pipeline together with the model;
2. reproduce the same preprocessing in C#;
3. preprocess the values in Grasshopper before connecting them to the
   component.

The supplied Random Forest sample uses raw `X1-X6` values and does not require
scaling.

---

# Part 2 — Replace the model

Replace:

```text
Model/model.onnx
```

with your ONNX file.

The easiest method is to keep the filename `model.onnx`. If you use another
filename, update this line in `PluginConfig.cs`:

```csharp
public const string ModelFileName = "model.onnx";
```

The Visual Studio project automatically copies the model beside the compiled
Grasshopper plugin.

---

# Part 3 — Edit `PluginConfig.cs`

Open:

```text
Plugin/PluginConfig.cs
```

Search for the word `EDIT`. There are six sections.

## EDIT 1 — Plugin and component names

Change the visible plugin information:

```csharp
public const string PluginName = "My Energy Model";
public const string ComponentName = "Energy Predictor";
public const string ComponentNickname = "Energy ML";
public const string Category = "DF2026";
public const string Subcategory = "My Models";
public const string AuthorName = "Student Name";
public const string AuthorContact = "student@example.com";
```

`Category` becomes the Grasshopper toolbar tab. `Subcategory` becomes the
panel within that tab.

## EDIT 2 — Generate unique GUIDs

Every plugin and every Grasshopper component must have stable, unique IDs.

In Visual Studio:

1. select **Tools > Create GUID**;
2. select **Registry Format**;
3. click **New GUID**;
4. click **Copy**;
5. paste the value into `AssemblyId`;
6. generate another GUID and paste it into `ComponentId`.

Example:

```csharp
public static readonly Guid AssemblyId =
    new Guid("YOUR-FIRST-GUID");

public static readonly Guid ComponentId =
    new Guid("YOUR-SECOND-GUID");
```

This step is mandatory when creating a new plugin.

After sharing a Grasshopper file, do not change `ComponentId`. Grasshopper uses
it to identify the component when reopening the file.

## EDIT 3 — Model filename

Keep:

```csharp
public const string ModelFileName = "model.onnx";
```

unless you changed the filename in the `Model` folder.

## EDIT 4 — ONNX tensor names

Use the names reported by ONNX Runtime:

```csharp
public const string InputTensorName = "float_input";
public const string OutputTensorName = "variable";
```

If the model has exactly one input and one output, you may use empty strings:

```csharp
public const string InputTensorName = "";
public const string OutputTensorName = "";
```

The plugin will then select the only available tensors automatically.

## EDIT 5 — Grasshopper inputs

Create one `PortDefinition` for every model feature, in the exact training
order:

```csharp
public static readonly PortDefinition[] Inputs =
{
    new PortDefinition(
        "Building Height",
        "H",
        "Building height in metres."),

    new PortDefinition(
        "Window Ratio",
        "WWR",
        "Window-to-wall ratio from 0 to 1."),

    new PortDefinition(
        "Orientation",
        "O",
        "Clockwise orientation in degrees.")
};
```

For this example, the ONNX input shape must be `[batch, 3]`.

Names make the component understandable. Nicknames should be short enough to
fit on the Grasshopper component.

## EDIT 6 — Grasshopper outputs

Create one definition for each value returned by the selected ONNX output
tensor:

```csharp
public static readonly PortDefinition[] Outputs =
{
    new PortDefinition(
        "Energy Use",
        "EUI",
        "Predicted annual energy use intensity."),

    new PortDefinition(
        "Peak Load",
        "Peak",
        "Predicted peak load.")
};
```

The number of configured outputs must equal the number of values returned for
one sample.

---

# Part 4 — Replace the icon

Replace:

```text
Assets/component_icon.png
```

with your own PNG image using the same filename.

Recommendations:

- use a square image;
- use a transparent background;
- use a simple shape that remains readable at small sizes;
- design at 24×24, 48×48, or a larger square size.

The project embeds this image into the GHA. `IconLoader.cs` automatically
scales and centres it on a 24×24 transparent canvas.

---

# Part 5 — Open and build in Visual Studio

## Requirements

- Rhino 8 for Windows;
- Visual Studio 2022;
- the **.NET desktop development** workload;
- an internet connection for the first NuGet restore.

## Build

1. Close Rhino if an older version of the plugin is loaded.
2. Open `Plugin/DF2026.PluginTemplate.sln`.
3. Wait for NuGet restore to finish.
4. Select `Debug` and `Any CPU`.
5. Choose **Build > Build Solution**.

Expected build output:

```text
Plugin/bin/Debug/net7.0-windows/win-x64/
```

The output should contain:

```text
DF2026.PluginTemplate.gha
Microsoft.ML.OnnxRuntime.dll
onnxruntime.dll
model.onnx
```

There may also be `.deps.json`, `.runtimeconfig.json`, PDB, and ONNX Runtime
provider files.

Do not distribute only the `.gha`; ONNX Runtime needs the other files.

---

# Part 6 — Test with Rhino and Grasshopper

1. In the Visual Studio run-profile menu, select
   **Rhino 8 - .NET 7**.
2. Press **F5**.
3. Rhino starts and opens Grasshopper.
4. Find the component under the category and subcategory configured in
   `PluginConfig.cs`.
5. Connect one number slider for each configured input.
6. Inspect the numeric outputs.
7. Connect a panel to `Model Info` and confirm:
   - model path;
   - input tensor name;
   - input shape;
   - output tensor name;
   - output shape.

`Model Path` is optional. Leave it empty to use the model copied beside the
GHA. During development, you can connect a different absolute ONNX path
without rebuilding.

## Validate against Python

Before distributing the plugin, select one row from the dataset and compare:

```text
Python prediction
vs.
Grasshopper prediction
```

Use the same feature values and feature order. The values should agree within
normal floating-point tolerance.

---

# Part 7 — Release and install

Change Visual Studio from `Debug` to `Release`, then build again.

Release output:

```text
Plugin/bin/Release/net7.0-windows/win-x64/
```

Copy the complete contents of that directory to:

```text
%AppData%\Grasshopper\Libraries\YourPluginName\
```

Restart Rhino.

If the project or compiled plugin was downloaded as a ZIP:

1. right-click the ZIP before extracting;
2. select **Properties**;
3. enable **Unblock**, if shown;
4. extract the ZIP again.

---

# Common errors

## The component does not appear

- confirm Rhino started with the .NET 7 runtime;
- confirm the complete output directory is available to Grasshopper;
- close all Rhino instances and rebuild;
- check whether another plugin uses the same GUID.

## `onnxruntime.dll` cannot be found

- copy the entire build output, not only the GHA;
- confirm Rhino and the plugin are both 64-bit;
- do not move the GHA away from its dependencies.

## Configured tensor was not found

The name in `PluginConfig.cs` does not match the ONNX model. Inspect the model
with the Python code in Part 1 and copy the exact name.

## Feature-count mismatch

The number of entries in `PluginConfig.Inputs` does not match the last
dimension of the ONNX input tensor.

Check both feature count and feature order.

## Output-count mismatch

The number of entries in `PluginConfig.Outputs` does not match the number of
values returned by the selected ONNX output tensor for one sample.

## Predictions run but are incorrect

Check:

- feature order;
- units;
- categorical encoding;
- missing-value handling;
- scaling and normalization;
- whether preprocessing was included in the ONNX model.

---

# Student completion checklist

- [ ] I replaced `Model/model.onnx`.
- [ ] I inspected the ONNX input and output metadata.
- [ ] I changed the plugin/component name and author.
- [ ] I generated two new GUIDs.
- [ ] I set the correct tensor names.
- [ ] I defined inputs in the training order.
- [ ] I defined the correct number of outputs.
- [ ] I replaced `Assets/component_icon.png`.
- [ ] Visual Studio builds with zero errors.
- [ ] The component appears in Grasshopper.
- [ ] `Model Info` shows the expected schema.
- [ ] A Grasshopper prediction matches Python.
- [ ] I copied the complete Release output when installing.

---

# 中文快速说明

学生通常只需要修改三个位置：

1. `Plugin/PluginConfig.cs`
2. `Model/model.onnx`
3. `Assets/component_icon.png`

在 `PluginConfig.cs` 中搜索 `EDIT`，依次修改插件名称、两个 GUID、模型文件名、
ONNX 输入输出 tensor 名称、Grasshopper 输入和输出。

然后打开 `Plugin/DF2026.PluginTemplate.sln`，等待 NuGet restore，选择
**Build > Build Solution**。按 F5 可以直接启动 Rhino 8 和 Grasshopper 调试。

安装插件时必须复制整个 Release 输出文件夹，不能只复制 `.gha`。
