﻿using PaleLuna.Architecture.Loops;

namespace PaleLuna.Patterns.State.Game
{
    /**
 * @brief Состояние паузы в игре.
 *
 * PauseState представляет состояние паузы в игре. При входе в это состояние вызывается метод OnPause для всех объектов,
 * зарегистрированных в pausablesHolder контроллера игры.
 */
    public class PauseState : GameStateBase
    {
        /**
        * @brief Конструктор класса.
        *
        * Инициализирует объект PauseState с заданным контекстом игры.
        *
        * @param context Объект GameController, представляющий текущий контекст игры.
        */
        public PauseState(GameLoops context) : base(context)
        {
        }

        /**
         * @brief Метод, вызываемый при начале состояния.
         *
         */
        public override void StateStart()
        {
            if(_context.GLConfig.tickMachineIsOn)
                _context.tickMachine.Stop();

            _context.pausablesHolder
                .ForEach(pausable => pausable.OnPause());
        }
            
    }
}
