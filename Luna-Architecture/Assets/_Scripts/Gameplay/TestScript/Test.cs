using System;
using UnityEngine;

public class Test : MonoBehaviour, IUpdatable, IStartable
{
    private ITypeCounter<Item> _itemCounter;

    public void OnStart()
    {
        //ServiceLocator.Instance.Get<GameController>()?.updatablesHolder.Registration(this);
        TestObjectCounter();

    }
        

    public void EveryFrameRun() => 
        Debug.Log($"Run {this}");


    private void TestObjectCounter()
    {
        _itemCounter = new ObjectCounter<Item>();

        try
        {
            _itemCounter.PopItems<Apple>();
        }
        catch (NullReferenceException ex)
        {
            print(ex.Message);
        }

        //���� �� ����������
        _itemCounter.AddItem(new Apple(), 10);
        _itemCounter.AddItem(new Rock());
        _itemCounter.AddItem(new Stick(),3);

        print(_itemCounter);

        //���� �� ��������
        _itemCounter.PopItems<Apple>(4);
        _itemCounter.PopItems<Stick>(5);
        _itemCounter.PopItems<Rock>(1);

        print(_itemCounter);

        _itemCounter.RemoveEmpty();
        print(_itemCounter);

        try
        {
            _itemCounter.CheckCount<Stick>();
        }
        catch (NullReferenceException ex)
        {
            print(ex.Message);
        }

        print(_itemCounter.PopItems<Apple>().EatApple());

    }

    private void OnDestroy() =>
        ServiceLocator.Instance?
            .Get<GameController>()
                .updatablesHolder?.UnRegistration(this);
}