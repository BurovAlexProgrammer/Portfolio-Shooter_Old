# Portfolio-Shooter

Проект основан на ранее разработанном модуле для быстрого старта проектов StarterPack (Ссылка на репозиторий StarterPack: https://github.com/BurovAlexProgrammer/StarterPack)

Демо шутер от первого лица. 

![CubeNukem_gameplay_v0 0 3](https://user-images.githubusercontent.com/7298288/215088006-4fcd69c4-4e75-40d7-a37e-99f2d0782b80.gif)

В проекте реализованы:
* Постэффекты.
* Используется только новая Input System.
* Примитивные анимации на DOTween.
* Сервис по автоматическому созданию пулов.
* Меню и игровой процесс на основе MachineState.
* Настройки с сериализацией в текстовый файл и хранение дефолтных настроек в ScriptableObjects.
* Zenject используется исключительно для загрузки сервисов на старте приложения и вторая группа сервисов при загрузке сцены.
* Локализация с автоматическим созданием ключей и редактор словаря.

Редактор локализации

![CubeNukem_localizationEditor_v0 0 3](https://user-images.githubusercontent.com/7298288/215090782-34911a4d-f940-4c3f-830d-8478fe27c338.png)
[Обновлено] Перевел графический API на Vulkan, проект на URP (Universal Rendering Pipline), Rendering Path переключен на Deferred для использования большого количества источников света.
![CubeNukem_gameplay_v0 1 1](https://user-images.githubusercontent.com/7298288/216840917-3f7bb8d6-ab74-41b8-909d-30f668f1e4e3.gif)


Addressable Groups

![CubeNukem_addressableGroups_v0 0 3](https://user-images.githubusercontent.com/7298288/215092390-514c34c4-70fc-416f-bfe5-739d2766b71f.png)

New InputSystem

![CubeNukem_inputSystem_v0 0 3](https://user-images.githubusercontent.com/7298288/215092401-0d70ed22-e65e-47d8-aba3-0fef6ae874dc.png)
