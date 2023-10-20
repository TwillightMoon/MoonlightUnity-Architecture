﻿using System;
using System.Collections.Generic;

namespace _Scripts.Other.Patterns.StatePattern
{
    public class StateHolder<T> : IStateHolder<T> where T : State
    {
        private Dictionary<Type, T> _stateMap;
        public T currentState;

        public StateHolder() => _stateMap = new Dictionary<Type, T>();


        public TP Register<TP>(TP state) where TP : T
        {
            Type type = state.GetType();

            if (_stateMap.ContainsKey(type))
                throw new Exception($"Cannot add item of type {type}. This type already exists");

            _stateMap[type] = state;

            return state;
        }

        public TP UnRegister<TP>() where TP : T
        {
            Type type = typeof(TP);

            if (!_stateMap.ContainsKey(type))
                throw new Exception($"item of type {type} doesn't exist in this map");

            TP item = (TP)_stateMap[type];
            _stateMap.Remove(type);

            return item;
        }

        public void ChangeState<TP>()
        {
            Type type = typeof(TP);
            
            if(!_stateMap.ContainsKey(type)) 
                throw new NullReferenceException($"State of type {type} doesn't exist in this map");
            
            currentState?.StateStop();
            currentState = _stateMap[type];
            currentState.StateStart();
        }
    }
}