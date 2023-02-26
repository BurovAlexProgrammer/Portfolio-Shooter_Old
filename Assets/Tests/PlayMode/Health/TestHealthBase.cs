using System;
using _Project.Scripts.Main;
using _Project.Scripts.Main.Game.Health;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Tests.PlayMode.Health
{
    public class TestHealthBase
    {
        [Test]
        public void WhenHealthInit_CurrentHealth50_AndMaxHealth40_CurrentHealthShouldBe40()
        {
            //Assign
            var health = Create.HealthBase();
            //Act
            health.Setup(50, 40);
            //Assert
            health.CurrentValue.Should().Be(40);
        }

        [Test]
        public void WhenTakeDamage10_ThenCurrentHealthShouldBeDecreased10()
        {
            //Assign
            var health = Create.HealthBase();
            health.DefaultSetup();
            var damage = 10;
            //Act
            health.TakeDamage(damage);
            //Assert
            health.CurrentValue.Should().Be(Constants.DefaultHealthCurrent - damage);
        }

        [Test]
        public void WhenTakeDamageMaxHealthValue_ThenDeadActionInvoked()
        {
            //Assign
            var damage = Constants.DefaultHealthMax; 
            var invokeTriggered = false;
            var health = Create.HealthBase();
            var eventMonitor = health.Monitor();
            health.DefaultSetup();
            //Act
            health.TakeDamage(damage);
            //Assert
            eventMonitor.Should().Raise(nameof(health.OnDead));
        }
        
        [Test]
        public void WhenTakeDamageMaxFloatValue_ThenDeadActionInvoked()
        {
            //Assign
            var damage = float.MaxValue; 
            var invokeTriggered = false;
            var health = Create.HealthBase();
            var eventMonitor = health.Monitor();
            health.DefaultSetup();
            //Act
            health.TakeDamage(damage);
            //Assert
            eventMonitor.Should().Raise(nameof(health.OnDead));
        }

        [Test]
        public void WhenTakeDamage01_ThenActionChangedInvoked()
        {
            //Assign
            var damage = 0.1f;
            var health = Create.HealthBase();
            var eventMonitor = health.Monitor();
            health.DefaultSetup();
            //Act
            health.TakeDamage(damage);
            //Assert
            eventMonitor.Should().Raise(nameof(health.OnChanged));
        }
        
        [Test]
        public void WhenCurrentHealth0_AndTakeDamage01_ThenActionChangedNotInvoked()
        {
            //Assign
            var damage = 0.1f;
            var health = Create.HealthBase();
            var eventMonitor = health.Monitor();
            health.Setup(0, Constants.DefaultHealthMax);
            //Act
            health.TakeDamage(damage);
            //Assert
            eventMonitor.Should().NotRaise(nameof(health.OnChanged));
        }
        
        [Test]
        public void WhenCurrentHealthNegative_AndTakeDamage01_ThenActionChangedNotInvoked()
        {
            //Assign
            var damage = 0.1f;
            var health = Create.HealthBase();
            var eventMonitor = health.Monitor();
            health.Setup(-10, Constants.DefaultHealthMax);
            //Act
            health.TakeDamage(damage);
            //Assert
            eventMonitor.Should().NotRaise(nameof(health.OnChanged));
        }

        [Test]
        public void WhenTakeDamage0_ThenActionChangedNotInvoked()
        {
            //Assign
            bool invokeTriggered = false;
            var health = Create.HealthBase();
            var eventMonitor = health.Monitor(); 
            health.DefaultSetup();
            //Act
            health.TakeDamage(0);
            //Assert
            eventMonitor.Should().NotRaise(nameof(health.OnChanged));
        }

        [Test]
        public void WhenTakeDamageNegative_ThenException()
        {
            //Assign
            var health = Create.HealthBase();
            health.Init(50, 50);
            //Act
            Action act = () => health.TakeDamage(-1);
            //Assert
            act.Should()
                .Throw<Exception>()
                .WithMessage(Messages.TakeDamageCannotBeNegative);
        }
    }
}