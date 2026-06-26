# Local Python 3.11 fallback

Google Colab is recommended. Use this local environment only when Colab is unavailable or unreliable.

## Conda method

From the root of this preparation package:

```bash
conda env create -f environment/environment.yml
conda activate surrogate-workshop
python --version
jupyter lab
```

To update an existing environment:

```bash
conda env update -n surrogate-workshop -f environment/environment.yml --prune
```

## pip method

Activate a Python 3.11 virtual environment, then run:

```bash
python -m pip install --upgrade pip
python -m pip install -r environment/requirements.txt
python -m ipykernel install --user --name surrogate-workshop --display-name "Python 3.11 (Surrogate Workshop)"
jupyter lab
```

Open `notebooks/Environment_Check.ipynb` and run all cells. No machine-learning model is trained by the check.
