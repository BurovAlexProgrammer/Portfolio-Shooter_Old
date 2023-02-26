using _Project.Scripts.Main.Game.Health;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

public class TestHealth
{
    [Test]
    public void WhenHealthInit_CurrentHealth50_AndMaxHealth40_CurrentHealthShouldBe40()
    {
        //Assign
        var health = Substitute.For<PlayerHealth>();
        //Act
        health.Init(50,40);
        //Assert
        health.CurrentValue.Should().Be(40);
    }

    [Test]
    public void WhenHealthTakeDamage10_AndCurrentHealth50_ThenCurrentHealthShouldBe40()
    {
        //Assign
        var health = Substitute.For<PlayerHealth>();
        health.Init(50,40);
        //Act
        health.TakeDamage(10);
        //Assert
        health.CurrentValue.Should().Be(30);
    }

    [Test]
    public void WhenHealthTakeDamage100_AndCurrentHealth100_ThenDeadActionInvoked()
    {
        //Assign
        bool invokeTriggered = false;
        var health = Substitute.For<PlayerHealth>();
        health.Init(100,100);
        health.Dead += HealthOnDead;
        //Act
        health.TakeDamage(100);
        //Assert
        invokeTriggered.Should().Be(true);
        
        void HealthOnDead()
        {
            invokeTriggered = true;
        }
    }


    [Test]
    public void WhenHealthTakeDamage1_ThenActionChangedInvoked()
    {
        //Assign
        bool invokeTriggered = false;
        var health = Substitute.For<PlayerHealth>();
        health.Init(50,50);
        health.Changed += HealthOnChanged;
        //Act
        health.TakeDamage(1);
        //Assert
        invokeTriggered.Should().Be(true);
        
        void HealthOnChanged(HealthBase health)
        {
            invokeTriggered = true;
        }
    }
}
