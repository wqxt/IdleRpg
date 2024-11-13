using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystemTests
{
    private Pawn _pawn;
    private PawnHealth _pawnHealth;
    private HealthView _healthView;
    private HealthObserver _healthObserver;

    [SetUp]
    public void Setup()
    {
        // �������������� Pawn � ��� ����������
        _pawn = new GameObject().AddComponent<Pawn>();
        _pawn.PawnConfiguration = ScriptableObject.CreateInstance<PawnConfiguration>();
        _pawnHealth = new PawnHealth(_pawn);

        // �������������� HealthView � ��� ����������
        _healthView = new GameObject().AddComponent<HealthView>();
        var maxHealthText = new GameObject().AddComponent<TextMeshProUGUI>();
        var currentHealthText = new GameObject().AddComponent<TextMeshProUGUI>();
        var healthSlider = new GameObject().AddComponent<Slider>();
        _healthView.Initialize(currentHealthText, maxHealthText, healthSlider);

        // �������������� HealthObserver � ��� ����������
        _healthObserver = new HealthObserver();
        _healthObserver._healthView = new[] { _healthView };
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(_pawn.gameObject);
        Object.DestroyImmediate(_pawn.PawnConfiguration);
        Object.DestroyImmediate(_healthView.gameObject);
        _healthObserver = null;
    }

    [TestCase(50)]
    [TestCase(100)]
    public void PawnHealthChangesWhenTakeDamage(int damage)
    {
        // Arrange

        // ������������� ��������� ��������
        _pawn.PawnConfiguration.Type = "Character";
        _pawn.PawnConfiguration.MaxHealthValue = 500;
        _pawn.PawnConfiguration.StartHealthValue = 300;
        _pawn.PawnConfiguration.CurrentHealthValue = 10;

        int initPawnHealth = _pawn.PawnConfiguration.CurrentHealthValue;

        // Act. ������� ���� ���� Character �� ���� Enemy.
        _pawnHealth.TakeDamage(damage, "Enemy"); 

        // Assert. �������� ����������� � �� ����� �������������.
        Assert.Less(_pawn.PawnConfiguration.CurrentHealthValue, initPawnHealth, "Health value did not decrease after taking damage."); 
        Assert.GreaterOrEqual(_pawn.PawnConfiguration.CurrentHealthValue, 0, "Health value should not be less than 0."); 
    }


    [TestCase(50)]
    [TestCase(100)]
    public void PawnHealthViewChangeWhenTakeDamage(int damage)
    {
        // Arrange

        // ������������� ��������� ��������
        _pawn.PawnConfiguration.Type = "Character";
        _pawn.PawnConfiguration.MaxHealthValue = 200;
        _pawn.PawnConfiguration.StartHealthValue = 100;
        _pawn.PawnConfiguration.CurrentHealthValue = 100;

        int initialCurrentPawnHealth = _pawn.PawnConfiguration.CurrentHealthValue;
        int initialMaxPawnHealth = _pawn.PawnConfiguration.MaxHealthValue;
        _healthView.SetupHealth(initialCurrentPawnHealth, initialMaxPawnHealth);
        _healthView.tag = "Character";


        // Act. ������� ���� ���� Character �� ���� Enemy.
        _pawnHealth.TakeDamage(damage, "Enemy");
        _healthObserver.UpdatePawnHealthView(_pawn.PawnConfiguration.CurrentHealthValue, _pawn.PawnConfiguration.Type);

        // �������� �������� ����� ����� 
        var updatedHealthTextValue = int.Parse(_healthView._currentHealthValueText.text);
        var updatedHealthSliderValue = _healthView._healthSlider.value;

        // Assert. ����������� �������� �������� ����������� � �� ����� �������������.
        Assert.Less(updatedHealthTextValue, initialCurrentPawnHealth, "Health text did not decrease after taking damage.");
        Assert.GreaterOrEqual(updatedHealthTextValue, 0, "Health text should not be less than 0.");

        //Assert. ����������� �������� �������� �������� ����������� � �� ����� �������������.
        Assert.Less(updatedHealthSliderValue, initialCurrentPawnHealth, "Health slider did not decrease after taking damage.");
        Assert.GreaterOrEqual(updatedHealthSliderValue, 0, "Health slider should not be less than 0.");
    }

    [TestCase(0)]
    [TestCase(-10)]
    [TestCase(-50)]
    public void HealthDoesNotGoNegativeWhenDamageIsNegative(int damage)
    {
        // ������������� ��������� ��������
        _pawn.PawnConfiguration.Type = "Character";
        _pawn.PawnConfiguration.MaxHealthValue = 200;
        _pawn.PawnConfiguration.StartHealthValue = 100;
        _pawn.PawnConfiguration.CurrentHealthValue = 100;

        // Arrange
        int initialHealth = _pawn.PawnConfiguration.CurrentHealthValue;

        // Act
        _pawnHealth.TakeDamage(damage, "Enemy");

        // Assert. �������� �� �������� ��� ������������� ������� ������.
        Assert.AreEqual(initialHealth, _pawn.PawnConfiguration.CurrentHealthValue, "Health should not decrease with negative damage."); 
    }

    [Test]
    public void TriggersPawnDeathEventWhenHealthZero()
    {
        // Arrange
        _pawn.PawnConfiguration.CurrentHealthValue = 10;
        _pawn.PawnConfiguration.Type = "Character";
        bool deathEventTrigger = false;
        _pawnHealth.PawnDeath += type => deathEventTrigger = true;

        // Act
        _pawnHealth.TakeDamage(10, "Enemy");

        // Assert. ������� ������ �������� ���������
        Assert.IsTrue(deathEventTrigger); 
    }
}