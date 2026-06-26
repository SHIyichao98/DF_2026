# 代理模型工作坊
## 软件与编程环境准备指南

请在工作坊开始前完成本指南中的准备工作。本次工作坊将涉及 Rhino/Grasshopper、表格数据、Python notebook、代理模型训练、模型解释，以及可选的 Grasshopper 组件开发。我们强烈建议使用 Google Colab；本地 Python 3.11 环境仅作为无法使用 Colab 时的备用方案。

> 推荐组合：Rhino 8 + Google Chrome 或 Microsoft Edge + Google Colab + Google Drive + GitHub + 一种 AI 辅助编程工具。

## 1. 准备工作清单

参加工作坊前，请确认你能够：

- 打开 Rhino 8 并启动 Grasshopper。
- 使用同一个 Google 账户登录 Google Colab 和 Google Drive。
- 在 Colab 中打开并运行 notebook。
- 运行 `notebooks/Environment_Check.ipynb` 中的全部单元格。
- 登录 GitHub 并访问代码仓库。
- 使用至少一种 AI 辅助编程或 debug 工具。
- 如果无法使用 Colab，能够从我们提供的 Python 3.11 环境启动 JupyterLab。
- 如果需要开发自定义 Grasshopper 组件，能够使用 Visual Studio 2022 编译并加载一个最小组件。

工作坊数据集和正式教学 notebook 将在之后另行提供。运行环境检查不需要提前准备数据集。

## 2. 电脑、操作系统与网络

### 推荐电脑配置

- 64 位 Windows 10 或 Windows 11 电脑兼容性最好。
- 至少 8 GB 内存，建议 16 GB 或更多。
- 安装 Rhino、Conda、Python 依赖和 Visual Studio 前，建议至少保留 15 GB 可用磁盘空间。
- 稳定的网络连接。
- 安装软件所需的管理员权限。
- 最新版本的 Chrome 或 Edge 浏览器。

Rhino 8 也支持较新的 macOS，但本指南中可选的 Visual Studio/Grasshopper 插件开发流程以 Windows 为准。使用 macOS 的参与者如果需要开发插件，请提前联系讲师。

官方参考：

- [Rhino 8 系统要求](https://www.rhino3d.com/8/system-requirements/)
- [下载 Google Chrome](https://www.google.com/chrome/)
- [下载 Microsoft Edge](https://www.microsoft.com/edge/download)

### 提前测试网络访问

请用工作坊当天将使用的网络打开以下服务：

- [Google Colab](https://colab.research.google.com/)
- [Google Drive](https://drive.google.com/)
- [GitHub](https://github.com/)
- 你选择的 AI 辅助编程工具

如果学校网络、VPN 政策、防火墙或地区限制导致某项服务无法访问，请提前按照第 5 节准备本地 JupyterLab 备用环境，不要等到工作坊开始后再处理。

## 3. Rhino 8 与 Grasshopper

### 必须完成的安装

1. 下载并安装 Rhino 8：
   - [Rhino 8 Windows 正式版](https://www.rhino3d.com/download/rhino-for-windows/8/latest/)
   - [Rhino 8 Windows 试用版](https://www.rhino3d.com/download/rhino-for-windows/8/evaluation/)
   - [Rhino 8 Mac 版](https://www.rhino3d.com/download/rhino-for-mac/8/latest/)
2. 如果没有许可证，请使用官方试用版。
3. 工作坊前至少启动一次 Rhino，并完成激活或账户登录。
4. 在 Rhino 命令行中运行 `Grasshopper`。
5. 确认 Grasshopper 画布能够正常打开且没有报错。
6. 在 Rhino 中运行 `SystemInfo` 命令；如果之后需要排错，请准备好复制其输出。

Grasshopper 已包含在 Rhino 8 中，正常情况下不需要单独安装。

常用官方资源：

- [Rhino 下载中心](https://www.rhino3d.com/download/)
- [Rhino 8 帮助文档](https://docs.mcneel.com/rhino/8/help/en-us/)
- [Grasshopper 开发指南](https://developer.rhino3d.com/guides/grasshopper/)
- [Rhino 与 Grasshopper 官方社区论坛](https://discourse.mcneel.com/)

### 版本要求

工作坊统一使用 Rhino 8。不要假定 Rhino 7 的插件、Python 脚本或 Grasshopper 文件在 Rhino 8 中一定具有完全相同的行为。如果电脑同时安装了 Rhino 7 和 Rhino 8，请确认工作坊文件是用 Rhino 8 打开的。

## 4. 推荐环境：Google Colab 与 Google Drive

### 账户准备

Colab 与 Drive 请使用同一个 Google 账户：

- [打开 Google Colab](https://colab.research.google.com/)
- [打开 Google Drive](https://drive.google.com/)
- [注册 Google 账户](https://accounts.google.com/signup)
- [Google Colab FAQ](https://research.google.com/colaboratory/faq.html)

请完成以下测试：

1. 登录 Google Drive。
2. 新建名为 `Surrogate_Model_Workshop` 的文件夹。
3. 打开 Google Colab 并新建一个 notebook。
4. 运行：

   ```python
   print("Colab is ready")
   ```

5. 将 `notebooks/Environment_Check.ipynb` 上传到 Colab。
6. 选择 **Runtime > Run all（运行时 > 全部运行）**。
7. 出现提示时，允许访问 Google Drive。
8. 确认最终报告显示 `READY`。

### Colab 套餐与学生优惠

Colab 免费版足以运行环境检查，预计也可以完成工作坊中的大部分练习。付费算力、GPU、高内存、使用额度和学生优惠可能因账户、国家或地区、学校以及活动时间而不同。请不要默认每位学生都会自动获得一年 Colab Pro。

参与者应登录自己的账户查看实际可用套餐：

- [Colab 付费套餐页面](https://colab.research.google.com/signup)
- [Colab FAQ：使用限制与计算资源](https://research.google.com/colaboratory/faq.html)

示例代码主要处理表格回归问题。Random Forest notebook 不需要 GPU；ANN notebook 也不应以始终能够获得 GPU 为前提。

### Google Drive 准备

- 如果可能，建议至少预留数 GB Drive 空间。
- Drive 与 Colab 必须使用同一个账户。
- notebook 运行期间不要移动或重命名正在使用的文件。
- 不要直接复制并使用其他参与者的 Drive 路径。
- 不要在共享 notebook 中保存密码、私密 API Key 或受限数据。

Google Drive 帮助：

- [将文件和文件夹上传到 Google Drive](https://support.google.com/drive/answer/2424368)
- [管理 Google 存储空间](https://support.google.com/drive/answer/6374270)

## 5. 本地备用环境：Python 3.11、Conda 与 JupyterLab

只有无法稳定使用 Colab 的参与者才需要安装完整本地环境。本工作坊统一支持本地 Python **3.11**。

### 选择 Conda 安装程序

Miniconda 体积较小，是我们的推荐选项；Anaconda Distribution 也可以使用。

- [Miniconda 安装指南](https://www.anaconda.com/docs/getting-started/miniconda/install)
- [Miniconda 下载](https://www.anaconda.com/download/success)
- [Anaconda Distribution 安装指南](https://www.anaconda.com/docs/getting-started/anaconda/install)
- [Conda 用户指南](https://docs.conda.io/projects/conda/en/latest/user-guide/)
- [JupyterLab 安装指南](https://jupyterlab.readthedocs.io/en/stable/getting_started/installation.html)

### 推荐方法：使用 `environment.yml`

Windows 用户打开 **Anaconda Prompt**，macOS 用户打开终端，进入本准备包所在目录，然后运行：

```bash
conda env create -f environment/environment.yml
conda activate surrogate-workshop
python --version
jupyter lab
```

输出的 Python 版本必须以 `3.11` 开头。

如果已经创建过环境，并且需要根据新版文件更新：

```bash
conda env update -n surrogate-workshop -f environment/environment.yml --prune
```

### 备用方法：使用 pip

创建并激活一个 Python 3.11 虚拟环境，然后运行：

```bash
python -m pip install --upgrade pip
python -m pip install -r environment/requirements.txt
python -m ipykernel install --user --name surrogate-workshop --display-name "Python 3.11 (Surrogate Workshop)"
jupyter lab
```

Windows 可通过以下命令创建标准虚拟环境：

```powershell
py -3.11 -m venv .venv
.\.venv\Scripts\Activate.ps1
```

macOS：

```bash
python3.11 -m venv .venv
source .venv/bin/activate
```

Python 下载与文档：

- [Python 3.11 文档](https://docs.python.org/3.11/)
- [Python 下载](https://www.python.org/downloads/)
- [Python 虚拟环境文档](https://docs.python.org/3.11/library/venv.html)

### 示例 notebook 涉及的 Python 包

提供的环境包含：

- NumPy
- pandas
- Matplotlib
- scikit-learn
- TensorFlow/Keras
- ONNX
- skl2onnx
- JupyterLab 与 ipykernel

环境检查会导入这些包并显示版本，但**不会**创建、训练、评估或导出任何机器学习模型。

各项目官方文档：

- [NumPy 安装](https://numpy.org/install/)
- [pandas 安装](https://pandas.pydata.org/docs/getting_started/install.html)
- [Matplotlib 安装](https://matplotlib.org/stable/install/index.html)
- [scikit-learn 安装](https://scikit-learn.org/stable/install.html)
- [TensorFlow pip 安装](https://www.tensorflow.org/install/pip)
- [ONNX](https://onnx.ai/)
- [skl2onnx 文档](https://onnx.ai/sklearn-onnx/)

## 6. 运行一键环境检查

检查文件位于：

```text
notebooks/Environment_Check.ipynb
```

它只执行准备状态检查：

- 判断当前运行于 Colab 还是本地 Jupyter。
- 显示操作系统和 Python 版本。
- 本地运行时确认是否为 Python 3.11。
- 导入需要的包并显示版本。
- 必要时将缺少的 Python 包安装到临时 Colab runtime；检查程序不会自动修改本地 Python 环境。
- 创建、读取并删除临时 CSV 文件。
- 在 Colab 中挂载并测试 Google Drive。
- 检查是否能够访问工作坊涉及的主要网站。
- 最后输出 `READY`、`READY WITH WARNINGS` 或 `NOT READY`。

### 在 Colab 中运行

1. 打开 [Google Colab](https://colab.research.google.com/)。
2. 选择 **File > Upload notebook（文件 > 上传笔记本）**。
3. 上传 `Environment_Check.ipynb`。
4. 选择 **Runtime > Run all（运行时 > 全部运行）**。
5. 允许 Drive 访问。
6. 保存最终报告或截图。

Colab 自行管理 Python 版本，因此我们不要求 Colab 当前必须使用 Python 3.11。Python 3.11 要求仅适用于本地备用环境。

Colab runtime 是临时环境。如果检查过程中安装了缺少的包，更换 runtime 后可能需要重新安装。

### 在 JupyterLab 中运行

1. 激活 `surrogate-workshop` 环境。
2. 运行 `jupyter lab`。
3. 打开 `notebooks/Environment_Check.ipynb`。
4. 选择 **Run > Run All Cells（运行 > 运行全部单元格）**。
5. 确认最终报告显示 `READY`。

如果本地环境有必需包导入失败，请运行：

```bash
conda activate surrogate-workshop
python -m pip install -r environment/requirements.txt
```

然后重启 notebook kernel 并重新运行全部单元格。

## 7. GitHub 账户与 Git

### 必须完成

1. 注册 GitHub 账户。
2. 验证邮箱。
3. 启用双重身份验证。
4. 确认能够登录，并打开讲师提供的 repository 链接。

官方说明：

- [创建 GitHub 账户](https://docs.github.com/en/get-started/start-your-journey/creating-an-account-on-github)
- [验证邮箱](https://docs.github.com/en/account-and-profile/how-tos/email-preferences/verifying-your-email-address)
- [设置双重身份验证](https://docs.github.com/en/authentication/securing-your-account-with-two-factor-authentication-2fa/configuring-two-factor-authentication)

### 推荐桌面工具

- [安装 Git](https://git-scm.com/downloads)
- [安装 GitHub Desktop](https://desktop.github.com/download/)
- [GitHub Desktop 文档](https://docs.github.com/en/desktop)
- [GitHub 入门教程](https://docs.github.com/en/get-started/start-your-journey/hello-world)

不熟悉命令行 Git 的参与者建议使用 GitHub Desktop。

### 文件管理规则

- 代码、notebook、文档和小型示例文件可以放在 GitHub。
- 大型数据集、模拟结果、Rhino 文件和训练后的模型文件通常应保存在 Google Drive，除非讲师另有说明。
- 不要提交密码、访问令牌或私密 API Key。
- 如果确实需要 Git LFS，请按照 [Git LFS 官方安装指南](https://docs.github.com/en/repositories/working-with-files/managing-large-files/installing-git-large-file-storage)操作。

## 8. AI 辅助编程与 Debug

请至少准备一种能够解释报错并协助修改 Python 代码的工具，例如：

- [ChatGPT](https://chatgpt.com/)
- [OpenAI Codex](https://openai.com/codex/)
- [GitHub Copilot](https://github.com/features/copilot)
- [Cursor](https://www.cursor.com/)
- [Claude](https://claude.ai/)

工作坊不强制要求特定工具或付费套餐，但请在工作坊前确认所选工具能够正常使用。

使用规则：

- 运行建议代码前，先阅读并理解修改内容。
- 求助时提供完整报错信息和最小必要代码段。
- 说明代码是在 Colab 还是本地环境中运行。
- 不要粘贴密码、API Key、私有仓库凭据、个人信息或受限研究数据。
- 将生成的代码视为需要验证的建议，而不是自动正确的答案。
- 接受大范围自动修改前保留可工作的版本。

## 9. 可选：开发 Grasshopper 组件

只有确实要开发自定义编译型 Grasshopper 组件的小组才需要完成本节。普通 Grasshopper definition 不需要 Visual Studio。

### Windows 开发环境

1. 安装 **Visual Studio 2022 Community**：
   - [Visual Studio Community 下载](https://visualstudio.microsoft.com/vs/community/)
   - [Visual Studio 安装文档](https://learn.microsoft.com/visualstudio/install/install-visual-studio)
2. 在 Visual Studio Installer 中勾选 **.NET desktop development（.NET 桌面开发）** workload。
3. 将 Visual Studio 更新到当前版本。
4. 按照 McNeel 当前的 Rhino 8/Grasshopper 组件教程，安装教程指定的模板或开发工具：
   - [Windows：创建第一个 Grasshopper 组件](https://developer.rhino3d.com/guides/grasshopper/your-first-component-windows/)
   - [Grasshopper 开发指南](https://developer.rhino3d.com/guides/grasshopper/)
   - [RhinoCommon API 文档](https://developer.rhino3d.com/api/rhinocommon/)
5. 编译教程中的最小示例组件。
6. 启动 Rhino 8 和 Grasshopper，确认组件能够找到并加载。

这里要求的是紫色图标的 **Visual Studio** 集成开发环境。**Visual Studio Code** 是另一个不同的软件，不能直接替代本节所述的 Visual Studio 开发环境。

如果参考旧教程，请确认它明确适用于 Rhino 8 和当前的 .NET 工具链。

### 插件排错

如果组件编译成功但在 Grasshopper 中找不到：

- 确认当前运行的是 Rhino 8，而不是 Rhino 7。
- 检查项目模板和 build target。
- 在 Rhino 中运行 `GrasshopperDeveloperSettings`，检查插件搜索路径。
- 检查 Windows 是否阻止了下载的 `.gha` 或 `.dll` 文件。
- 重新编译或修改插件路径后重启 Rhino。
- 求助时提供完整编译错误以及 Rhino `SystemInfo` 输出。

## 10. 数据与文件组织

数据集将在之后发放。现在可以先创建以下 Drive 目录：

```text
My Drive/
└── Surrogate_Model_Workshop/
    ├── data/
    ├── notebooks/
    ├── grasshopper/
    ├── models/
    └── outputs/
```

建议遵守以下规则：

- `data/` 中保留未经修改的原始数据。
- 修改后的 notebook 使用新文件名保存。
- 文件夹名称尽量简短，避免使用特殊标点。
- 本地运行时尽可能使用相对路径。
- 在 Colab 中，路径通常以以下内容开头：

  ```text
  /content/drive/MyDrive/Surrogate_Model_Workshop/
  ```

- Python 区分 CSV 列名的大小写。示例 notebook 使用类似 `X1` 至 `X6` 的输入特征名和类似 `Y1` 的目标列名；最终数据结构将与数据集一起提供。

## 11. 工作坊前最终测试

请至少提前两到三天完成以下测试：

1. Rhino 8 可以启动，Grasshopper 可以正常打开。
2. Google Colab 和 Google Drive 可以访问。
3. `Environment_Check.ipynb` 能够运行到最终报告。
4. 在 Colab 中，Drive 写入检查通过。
5. GitHub 登录和邮箱验证已经完成。
6. AI 编程工具能够打开并接受一条简单测试提示。
7. 如果无法使用 Colab，本地 Python 3.11 环境检查能够通过。
8. 如果需要开发组件，Visual Studio 能够编译并加载示例组件。

请保留环境检查的最终输出。如果结果不是 `READY`，请在到场前将完整输出发给工作坊讲师。

## 12. 快速故障排查

### Colab 无法挂载 Drive

- 确认 Colab 与 Drive 使用同一个 Google 账户。
- 允许 Colab 页面打开弹窗并进行 Google 登录。
- 断开旧 runtime 后重试。
- 阅读 [Colab FAQ](https://research.google.com/colaboratory/faq.html)。

### Jupyter 使用了错误的 Python

运行：

```bash
conda activate surrogate-workshop
python --version
python -m ipykernel install --user --name surrogate-workshop --display-name "Python 3.11 (Surrogate Workshop)"
```

然后在 JupyterLab 中选择该 kernel。

### Python 包导入失败

运行：

```bash
conda activate surrogate-workshop
python -m pip install -r environment/requirements.txt
```

完成后重启 kernel。

### PowerShell 阻止虚拟环境激活

可以改用 Anaconda Prompt 和 Conda 环境方案。未经许可，不要为了安装环境而降低学校或机构的整体安全策略。

### TensorFlow 没有检测到 GPU

环境检查不要求 GPU。示例 notebook 可以在 CPU 上运行。本地 GPU 配置不属于工作坊必须完成的准备工作。

### 浏览器无法访问 Google 或 GitHub

请测试另一个符合规定的网络，并联系学校 IT 支持。同时准备本地备用环境，并提前取得工作坊文件。
