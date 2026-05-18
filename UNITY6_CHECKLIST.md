# Unity 6 Migration Checklist

## После открытия проекта в Unity 6

### 1. Compilation Check
- [ ] Проект компилируется без ошибок
- [ ] Нет критичных warnings
- [ ] Все скрипты загружены

### 2. Package Manager
- [ ] Все пакеты обновлены успешно
- [ ] Photon PUN 2 виден в Assets/Photon
- [ ] Input System работает
- [ ] URP установлен корректно

### 3. Project Settings
- [ ] **Graphics:** URP pipeline asset назначен
- [ ] **Quality:** URP settings применены
- [ ] **Input System:** Backend = Both или New
- [ ] **Player:** API Compatibility Level проверен

### 4. Scene Check
- [ ] Открыть `NewGameLobby` сцену
- [ ] Открыть `MainLevel` сцену
- [ ] Проверить освещение (может потребоваться rebake)
- [ ] Проверить материалы (URP upgrade)

### 5. Photon PUN 2 Test
- [ ] Window → Photon Unity Networking → PUN Wizard открывается
- [ ] App ID сохранен
- [ ] Можно подключиться к Photon Cloud
- [ ] Лобби загружается

### 6. Gameplay Test (Editor)
- [ ] Запустить `NewGameLobby` сцену
- [ ] Подключиться к серверу
- [ ] Создать комнату
- [ ] Запустить матч
- [ ] Движение работает (WASD)
- [ ] Атака работает (ЛКМ)
- [ ] Переключение атак (1/2)
- [ ] UI отображается корректно

### 7. Multiplayer Test
- [ ] Build проекта успешен
- [ ] Запустить 2 клиента (Editor + Build)
- [ ] Оба видят друг друга
- [ ] Синхронизация движения
- [ ] Урон синхронизируется
- [ ] Счет обновляется

### 8. Performance Check
- [ ] FPS в редакторе стабильный
- [ ] Нет memory leaks
- [ ] Profiler показывает норму

## Известные проблемы Unity 6 + PUN 2

### Если не компилируется:
```
- Проверить Scripting Backend (IL2CPP vs Mono)
- API Compatibility Level = .NET Standard 2.1
```

### Если URP материалы розовые:
```
Edit → Render Pipeline → Universal Render Pipeline → Upgrade Project Materials
```

### Если Input System не работает:
```
Edit → Project Settings → Player → Active Input Handling = Both
Перезапустить Unity
```

### Если Photon не подключается:
```
- Проверить App ID в PhotonServerSettings
- Проверить интернет соединение
- Проверить Photon Dashboard (не истек ли план)
```

## После успешной миграции

- [ ] Закоммитить изменения
- [ ] Обновить README.md (указать Unity 6)
- [ ] Создать build для тестирования
- [ ] Запушить в репозиторий

## Если что-то сломалось

```bash
cd C:\Users\stasy\RoboStars
git checkout main
```

Откатиться на Unity 2021.3.30f1 и сообщить о проблеме.
