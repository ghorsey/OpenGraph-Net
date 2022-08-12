# Setup info

1. Issue the following git command `git worktree add --checkout ../gh-pages gh-pages` in the git root
1. `dotnet build` to build the dll for the metadata extraction.
1. `docfx metadata` to generate the latest metadata.
1. `docfx build` to build the site into the gh-pages worktree location.
1. `docfx --serve` to serve the site from the gh-pages worktree location.
1. If it all looks good, navigate the console to `../gh-pages` and commit/push the site updates.

minor edit that should not trigger a build.

## Troubleshooting

### There was an issue building the metadata

I had to modify the metadata section of the docfx.json file from:

```json
{
  "metadata": [
    {
      "src": [
        {
          "files": [
            "src/**.csproj"
          ],
          "src": "../"
        }
      ],
      "dest": "api",
      "disableGitFeatures": false
    }
  ],
  // ..
}
```

to

```json
{
  "metadata": [
    {
      "src": [
        {
          "files": [
            "OpenGraphNet.dll"
          ],
          "src": "../src/OpenGraphNet/bin/Debug/net6.0"
        }
      ],
      "dest": "api",
      "disableGitFeatures": false
    }
  ],
  // ...
}
```
