# Surrogate Modeling Workshop
## Software and Programming Environment Preparation Guide

Please complete this setup **before the workshop**. The workshop will combine Rhino/Grasshopper, tabular datasets, Python notebooks, surrogate-model training, model interpretation, and optional Grasshopper component development. A working Google Colab environment is strongly recommended; a local Python 3.11 environment is the fallback.

> Recommended route: Rhino 8 + Google Chrome or Microsoft Edge + Google Colab + Google Drive + GitHub + one AI coding assistant.

## 1. Preparation checklist

Before attending, confirm that you can:

- Open Rhino 8 and start Grasshopper.
- Sign in to Google Colab and Google Drive with the same Google account.
- Open and run a notebook in Colab.
- Run every cell in `notebooks/Environment_Check.ipynb`.
- Sign in to GitHub and access a repository.
- Use at least one AI-assisted coding/debugging tool.
- If you cannot use Colab, launch JupyterLab from the supplied Python 3.11 environment.
- If you will develop a custom Grasshopper component, compile and load a minimal component with Visual Studio 2022.

The datasets and workshop teaching notebooks will be provided later. You do not need a dataset to run the environment check.

## 2. Computer, operating system, and network

### Recommended computer

- A 64-bit Windows 10 or Windows 11 computer is the most compatible option.
- At least 8 GB RAM; 16 GB or more is recommended.
- At least 15 GB of free disk space before installing Rhino, Conda, Python packages, and Visual Studio.
- A stable internet connection.
- Administrator permission to install software.
- An up-to-date Chrome or Edge browser.

Rhino 8 also supports recent macOS versions, but the optional Visual Studio/Grasshopper plug-in development instructions in this guide are written for Windows. Participants using macOS should inform the instructors in advance.

Official references:

- [Rhino 8 system requirements](https://www.rhino3d.com/8/system-requirements/)
- [Google Chrome download](https://www.google.com/chrome/)
- [Microsoft Edge download](https://www.microsoft.com/edge/download)

### Network access to test

Before the workshop, open each service from the network you will use:

- [Google Colab](https://colab.research.google.com/)
- [Google Drive](https://drive.google.com/)
- [GitHub](https://github.com/)
- Your chosen AI coding assistant

If any service is blocked by an institutional network, VPN policy, firewall, or regional restriction, prepare the local JupyterLab fallback described in Section 5. Do not wait until the workshop begins.

## 3. Rhino 8 and Grasshopper

### Required installation

1. Download and install Rhino 8:
   - [Rhino 8 for Windows](https://www.rhino3d.com/download/rhino-for-windows/8/latest/)
   - [Rhino 8 evaluation for Windows](https://www.rhino3d.com/download/rhino-for-windows/8/evaluation/)
   - [Rhino 8 for Mac](https://www.rhino3d.com/download/rhino-for-mac/8/latest/)
2. If you do not have a license, use the official evaluation version.
3. Start Rhino once before the workshop and complete activation/sign-in.
4. In Rhino, run the `Grasshopper` command.
5. Confirm that the Grasshopper canvas opens without an error.
6. Run Rhino's `SystemInfo` command and keep the output available in case troubleshooting is needed.

Grasshopper is included with Rhino 8; it does not require a separate standard installation.

Useful official resources:

- [Rhino downloads](https://www.rhino3d.com/download/)
- [Rhino documentation](https://docs.mcneel.com/rhino/8/help/en-us/)
- [Grasshopper guides](https://developer.rhino3d.com/guides/grasshopper/)
- [Rhino and Grasshopper community forum](https://discourse.mcneel.com/)

### Version policy

Use Rhino 8 for the workshop. Do not assume that a Rhino 7 plug-in, Python script, or Grasshopper definition will behave identically in Rhino 8. If you already have both versions installed, verify that workshop files open specifically in Rhino 8.

## 4. Recommended environment: Google Colab and Google Drive

### Accounts

Use one Google account for both services:

- [Open Google Colab](https://colab.research.google.com/)
- [Open Google Drive](https://drive.google.com/)
- [Create a Google account](https://accounts.google.com/signup)
- [Colab FAQ](https://research.google.com/colaboratory/faq.html)

Complete the following test:

1. Sign in to Google Drive.
2. Create a folder named `Surrogate_Model_Workshop`.
3. Open Google Colab and create a new notebook.
4. Run:

   ```python
   print("Colab is ready")
   ```

5. Upload `notebooks/Environment_Check.ipynb` to Colab.
6. Select **Runtime > Run all**.
7. Approve Google Drive access when prompted.
8. Confirm that the final report says `READY`.

### Colab plans and student offers

The free Colab tier is sufficient for the environment check and is expected to be sufficient for most workshop exercises. Paid compute availability, GPU access, usage limits, and student promotions can vary by account, country, institution, and date. Do not assume that every student automatically receives one year of Colab Pro.

Participants should check the plan shown in their own account:

- [Colab paid plans](https://colab.research.google.com/signup)
- [Google Colab FAQ: usage limits and resources](https://research.google.com/colaboratory/faq.html)

The example workshop code is primarily tabular regression. A GPU is not required for the Random Forest notebook and should not be treated as guaranteed for the ANN notebook.

### Google Drive preparation

- Keep at least several gigabytes of free Drive storage if possible.
- Use the same account in Drive and Colab.
- Do not rename or move workshop files while a notebook is running.
- Do not hard-code another participant's Drive path.
- Never place passwords, private API keys, or confidential data in a shared notebook.

Google Drive help:

- [Upload files and folders to Google Drive](https://support.google.com/drive/answer/2424368)
- [Manage Google storage](https://support.google.com/drive/answer/6374270)

## 5. Local fallback: Python 3.11, Conda, and JupyterLab

Only participants who cannot reliably use Colab need the full local environment. Python **3.11** is the supported local version for this workshop.

### Choose a Conda installer

Miniconda is smaller and is the recommended installer. Anaconda Distribution is also acceptable.

- [Miniconda installation guide](https://www.anaconda.com/docs/getting-started/miniconda/install)
- [Miniconda downloads](https://www.anaconda.com/download/success)
- [Anaconda Distribution installation guide](https://www.anaconda.com/docs/getting-started/anaconda/install)
- [Conda user guide](https://docs.conda.io/projects/conda/en/latest/user-guide/)
- [JupyterLab installation guide](https://jupyterlab.readthedocs.io/en/stable/getting_started/installation.html)

### Recommended installation using `environment.yml`

Open **Anaconda Prompt** on Windows, or a terminal on macOS, change to this preparation-package directory, and run:

```bash
conda env create -f environment/environment.yml
conda activate surrogate-workshop
python --version
jupyter lab
```

The Python version must begin with `3.11`.

If the environment already exists and the file has been updated:

```bash
conda env update -n surrogate-workshop -f environment/environment.yml --prune
```

### Alternative pip installation

Create and activate a Python 3.11 virtual environment, then run:

```bash
python -m pip install --upgrade pip
python -m pip install -r environment/requirements.txt
python -m ipykernel install --user --name surrogate-workshop --display-name "Python 3.11 (Surrogate Workshop)"
jupyter lab
```

On Windows, a standard virtual environment can be created with:

```powershell
py -3.11 -m venv .venv
.\.venv\Scripts\Activate.ps1
```

On macOS:

```bash
python3.11 -m venv .venv
source .venv/bin/activate
```

Python downloads and documentation:

- [Python 3.11 documentation](https://docs.python.org/3.11/)
- [Python downloads](https://www.python.org/downloads/)
- [Python virtual environments](https://docs.python.org/3.11/library/venv.html)

### Packages represented in the example notebooks

The supplied environment contains:

- NumPy
- pandas
- Matplotlib
- scikit-learn
- TensorFlow/Keras
- ONNX
- skl2onnx
- JupyterLab and ipykernel

The environment check imports these packages and reports their versions, but it does **not** create, train, evaluate, or export a machine-learning model.

Official package documentation:

- [NumPy installation](https://numpy.org/install/)
- [pandas installation](https://pandas.pydata.org/docs/getting_started/install.html)
- [Matplotlib installation](https://matplotlib.org/stable/install/index.html)
- [scikit-learn installation](https://scikit-learn.org/stable/install.html)
- [TensorFlow pip installation](https://www.tensorflow.org/install/pip)
- [ONNX](https://onnx.ai/)
- [skl2onnx documentation](https://onnx.ai/sklearn-onnx/)

## 6. Run the one-click environment check

The file is:

```text
notebooks/Environment_Check.ipynb
```

It performs only readiness checks:

- Detects Colab versus local Jupyter.
- Reports the operating system and Python version.
- Confirms Python 3.11 for local execution.
- Imports required packages and reports versions.
- Installs a missing required Python package into the temporary Colab runtime when necessary; it does not automatically modify a local environment.
- Creates, reads, and removes a temporary CSV file.
- Mounts and tests Google Drive when running in Colab.
- Checks access to key workshop websites.
- Prints a final `READY`, `READY WITH WARNINGS`, or `NOT READY` summary.

### In Colab

1. Open [Google Colab](https://colab.research.google.com/).
2. Select **File > Upload notebook**.
3. Upload `Environment_Check.ipynb`.
4. Select **Runtime > Run all**.
5. Approve Drive access.
6. Save or screenshot the final report.

Colab controls its own Python version, so the notebook does not require Colab to be Python 3.11. The Python 3.11 requirement applies to the local fallback.

Colab runtimes are temporary. If the check installs a missing package, Colab may need to install it again after a new runtime is assigned.

### In JupyterLab

1. Activate `surrogate-workshop`.
2. Run `jupyter lab`.
3. Open `notebooks/Environment_Check.ipynb`.
4. Select **Run > Run All Cells**.
5. Confirm that the final report says `READY`.

If a required import fails locally, run:

```bash
conda activate surrogate-workshop
python -m pip install -r environment/requirements.txt
```

Then restart the notebook kernel and run all cells again.

## 7. GitHub account and Git

### Required

1. Create a GitHub account.
2. Verify your email address.
3. Enable two-factor authentication.
4. Confirm that you can sign in and open a repository link supplied by the instructors.

Official instructions:

- [Create a GitHub account](https://docs.github.com/en/get-started/start-your-journey/creating-an-account-on-github)
- [Verify an email address](https://docs.github.com/en/account-and-profile/how-tos/email-preferences/verifying-your-email-address)
- [Configure two-factor authentication](https://docs.github.com/en/authentication/securing-your-account-with-two-factor-authentication-2fa/configuring-two-factor-authentication)

### Recommended desktop tools

- [Install Git](https://git-scm.com/downloads)
- [Install GitHub Desktop](https://desktop.github.com/download/)
- [GitHub Desktop documentation](https://docs.github.com/en/desktop)
- [GitHub introductory tutorial](https://docs.github.com/en/get-started/start-your-journey/hello-world)

GitHub Desktop is recommended for participants who are not comfortable with command-line Git.

### File policy

- Code, notebooks, documentation, and small examples may be kept in GitHub.
- Large datasets, generated simulation results, Rhino files, and trained-model binaries should normally stay in Google Drive unless the instructors specify otherwise.
- Never commit passwords, access tokens, or private API keys.
- If Git LFS is required, follow the [official Git LFS installation guide](https://docs.github.com/en/repositories/working-with-files/managing-large-files/installing-git-large-file-storage).

## 8. AI-assisted coding and debugging

Prepare at least one tool that can explain errors and help edit Python code. Examples include:

- [ChatGPT](https://chatgpt.com/)
- [OpenAI Codex](https://openai.com/codex/)
- [GitHub Copilot](https://github.com/features/copilot)
- [Cursor](https://www.cursor.com/)
- [Claude](https://claude.ai/)

The workshop does not require a particular paid plan. Confirm that your selected tool works before the workshop.

Responsible-use rules:

- Read and understand suggested changes before running them.
- Share the complete error message and the smallest relevant code section.
- State whether the notebook is running in Colab or locally.
- Never paste passwords, API keys, private repository credentials, personal information, or restricted research data.
- Treat generated code as a proposal that must be tested.
- Keep a working copy before accepting large automated edits.

## 9. Optional: Grasshopper component development

This section is required only if your group will build a custom compiled Grasshopper component. It is not required for ordinary Grasshopper definitions.

### Windows development setup

1. Install **Visual Studio 2022 Community**:
   - [Visual Studio Community download](https://visualstudio.microsoft.com/vs/community/)
   - [Visual Studio installation documentation](https://learn.microsoft.com/visualstudio/install/install-visual-studio)
2. In the Visual Studio Installer, select the **.NET desktop development** workload.
3. Keep Visual Studio updated.
4. Follow McNeel's current Rhino 8/Grasshopper component guide and install the template or tooling it specifies:
   - [Your first Grasshopper component for Windows](https://developer.rhino3d.com/guides/grasshopper/your-first-component-windows/)
   - [Grasshopper developer guides](https://developer.rhino3d.com/guides/grasshopper/)
   - [RhinoCommon API documentation](https://developer.rhino3d.com/api/rhinocommon/)
5. Build the minimal sample component.
6. Start Rhino 8, open Grasshopper, and confirm that the component appears.

**Visual Studio** is the purple integrated development environment. **Visual Studio Code** is a different application and does not replace the Visual Studio setup described here.

When following older tutorials, verify that the instructions explicitly apply to Rhino 8 and the current .NET tooling.

### Plug-in troubleshooting

If a component compiles but does not appear:

- Confirm that Rhino 8, not Rhino 7, is running.
- Check the build target and project template.
- Use Rhino's `GrasshopperDeveloperSettings` command to review search folders.
- Check whether Windows has blocked a downloaded `.gha` or `.dll` file.
- Restart Rhino after rebuilding or changing a plug-in search path.
- Copy the exact build error and Rhino `SystemInfo` output when asking for help.

## 10. Data and file organization

The datasets will be distributed later. Create this Drive structure now:

```text
My Drive/
└── Surrogate_Model_Workshop/
    ├── data/
    ├── notebooks/
    ├── grasshopper/
    ├── models/
    └── outputs/
```

Recommended rules:

- Keep original datasets unchanged in `data/`.
- Save edited notebooks under a new name.
- Use short folder names without unusual punctuation.
- Use relative paths locally whenever possible.
- In Colab, paths will normally begin with:

  ```text
  /content/drive/MyDrive/Surrogate_Model_Workshop/
  ```

- CSV column names are case-sensitive in Python. The example notebooks expect feature names such as `X1` through `X6` and a target such as `Y1`; final dataset schemas will be provided separately.

## 11. Final pre-workshop test

Complete this test at least two or three days before the workshop:

1. Rhino 8 starts and Grasshopper opens.
2. Google Colab and Google Drive are accessible.
3. `Environment_Check.ipynb` reaches its final summary.
4. The Drive write test passes in Colab.
5. GitHub sign-in and email verification are complete.
6. Your AI coding assistant opens and accepts a simple test prompt.
7. If Colab is unavailable, the Python 3.11 local notebook passes.
8. If developing components, Visual Studio builds and loads the sample component.

Keep the final environment-check output. If the result is not `READY`, send the complete output to the workshop instructors before arrival.

## 12. Quick troubleshooting

### Colab cannot mount Drive

- Confirm that Colab and Drive use the same Google account.
- Allow pop-ups and Google sign-in for the Colab page.
- Disconnect old runtimes and try again.
- Review the [Colab FAQ](https://research.google.com/colaboratory/faq.html).

### Jupyter uses the wrong Python

Run:

```bash
conda activate surrogate-workshop
python --version
python -m ipykernel install --user --name surrogate-workshop --display-name "Python 3.11 (Surrogate Workshop)"
```

Then select that kernel from JupyterLab.

### A package import fails

Run:

```bash
conda activate surrogate-workshop
python -m pip install -r environment/requirements.txt
```

Restart the kernel afterward.

### PowerShell blocks virtual-environment activation

Use Anaconda Prompt and the Conda environment method. Participants should not weaken organization-wide security policies without authorization.

### TensorFlow does not detect a GPU

GPU detection is not required for environment readiness. The notebooks can run on CPU. Local GPU configuration is outside the required workshop setup.

### The browser cannot reach Google or GitHub

Test another approved network and contact your institution's IT support. Prepare the local fallback and obtain workshop materials in advance.
