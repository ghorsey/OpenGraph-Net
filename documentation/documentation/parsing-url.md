
# Parsing from a URL

Synchronously parse a url:

```csharp
OpenGraph graph = OpenGraph.ParseUrl("https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe/playlist/2lzTTRqhYS6AkHPIvdX9u3?si=KcZxfwiIR7OBPCzj20utaQ");
```

Use `async/await` to parse a url:
```csharp
OpenGraph graph = await OpenGraph.ParseUrlAsync("https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe/playlist/2lzTTRqhYS6AkHPIvdX9u3?si=KcZxfwiIR7OBPCzj20utaQ");
```
