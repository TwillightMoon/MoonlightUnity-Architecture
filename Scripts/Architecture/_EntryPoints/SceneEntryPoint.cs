﻿using Cysharp.Threading.Tasks;
using Services;
using UnityEngine;

namespace PaleLuna.Architecture.EntryPoint
{
    /**
 * @brief Класс для точек входа сцен.
 *
 * Этот класс предоставляет базовую структуру для управления инициализацией компонентов при запуске сцены.
 */
    [AddComponentMenu("Moonlight Unity / Entry Points / Scene Boot")]
    public class SceneEntryPoint : EntryPoint
    {
        protected ServiceLocator _sceneServiceLocator = new ServiceLocator();

        /**
        * @brief Асинхронный метод для настройки и запуска игры.
        *
        * Создает объект "DontDestroy" для предотвращения уничтожения при переходе между сценами.
        * Инициализирует ServiceLocator, заполняет и запускает инициализаторы, загружает сервисы,
        * изменяет состояние игры, компилирует и запускает компоненты IStartable, переходит к следующей сцене.
        */
        protected override async UniTask Setup()
        {
            ServiceManager.Instance.SceneLocator = _sceneServiceLocator;
            _sceneServiceLocator.Registarion<Apple>(new Apple());
            FillSceneLocator();

            await UniTask.Yield();

            await base.Setup();

            ProcessBaggage();
        }

        protected virtual void FillSceneLocator() { }

        protected virtual void ProcessBaggage() { }
    }
}
