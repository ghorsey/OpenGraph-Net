# Setup info

1. Issue the following git command `git worktree add --checkout ../gh-pages gh-pages` in the git root
2. `docfx build` to build the site into the gh-pages worktree location.
3. `docfx --serve` to serve the site from the gh-pages worktree location.
4. If it all looks good, navigate the console to `../gh-pages` and commit/push the site updates.
