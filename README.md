# Typo Fixer — Spell Checker & Autocomplete (C#)

A console application for detecting and correcting word spelling errors, 
implemented using dynamic programming techniques.

## Features

- **Dictionary-based word checking** — splits the input sentence into words, 
  strips punctuation, and checks each word against a dictionary (`words_list`, based on SCOWL).
- **Longest Common Subsequence (LCS) algorithm** — finds correction candidates 
  based on the longest common subsequence.
- **Levenshtein distance** — classic implementation (insertion/deletion/substitution) 
  for finding the closest candidate words.
- **Modified Levenshtein distance with transposition support** — additional handling 
  of common typos (e.g., "Daer" → "Dear", swapped adjacent letters).
- **Prefix-based autocomplete (TAB)** — bonus feature: suggests word completions 
  based on the prefix already typed.

## Tech Stack

C#, .NET 10.0

## How to Run

```bash
dotnet run --project TypoFixer
```

Enter any sentence — the program will highlight potentially misspelled words 
and suggest corrections. Press TAB while typing to trigger autocomplete.

## Project Structure

- `DictionaryLoader.cs` — loads the word dictionary
- `Suggestor.cs` — generates suggestions (LCS + Levenshtein)
- `Program.cs` — entry point, handles user input

## Team Project

This project was completed in pairs as part of the CS210 Algorithms for Engineers course.

**My contribution (sofiiakapsha):** project setup, dictionary loading, 
LCS algorithm implementation, Levenshtein distance with transposition modification, 
suggestion generation, bug fixes.

**Partner's contribution (yevheniiakrychun25):** implementation of the autocomplete (TAB) feature.
