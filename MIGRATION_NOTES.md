# Unity 6 Migration Notes

## Migration Date
2026-05-18

## Changes Made

### Unity Version
- **From:** Unity 2021.3.30f1
- **To:** Unity 6.0.x (6000.x)

### Package Updates

| Package | Old Version | New Version |
|---------|-------------|-------------|
| Input System | 1.7.0 | 1.11.2 |
| URP | 12.1.12 | 17.0.3 |
| TextMeshPro | 3.0.6 | 3.2.0 |
| Timeline | 1.6.5 | 1.8.7 |
| uGUI | 1.0.0 | 2.0.0 |
| Visual Scripting | 1.8.0 | 1.9.4 |
| Rider | 3.0.24 | 3.0.31 |
| VS | 2.0.18 | 2.0.22 |
| Test Framework | 1.1.33 | 1.4.5 |
| Collab Proxy | 2.0.5 | 2.5.2 |

### Network Stack
- **Photon PUN 2:** Kept (no changes)
- Will verify compatibility after Unity upgrade

## Next Steps

1. Open project in Unity 6.0.x
2. Let Unity upgrade project automatically
3. Check for compilation errors
4. Test Photon PUN 2 connectivity
5. Update URP renderer settings if needed
6. Test gameplay in editor
7. Build and test standalone

## Known Issues to Watch

- [ ] Input System bindings may need regeneration
- [ ] URP shaders may need upgrade
- [ ] Photon PUN 2 compatibility check
- [ ] Custom properties serialization
- [ ] Scene lighting may need rebaking

## Rollback Plan

If migration fails:
```bash
git checkout main
```

Backup is on `main` branch.
