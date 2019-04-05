using System;
using DILib;
using DILibTests.Classes;
using DILibTests.Interfaces;
using Xunit;

namespace DILibTests
{
    public class ContainerTests
    {
        private readonly IContainer _container = new Container();
        

        [Fact]
        public void RegisterBareImplementationTest()
        {
            var container = _container.Resolve<IContainer>();

            container.RegisterTransient<Thing>();
            Assert.True(container.IsRegistered<Thing>());
        }


        [Fact]
        public void RegisterAbstractionImplementationTest()
        {
            var container = _container.Resolve<IContainer>();

            container.RegisterTransient<IThing, Thing>();
            Assert.True(container.IsRegistered<IThing>());
        }


        [Fact]
        public void RegisterRegisteredTypeWithoutRewritingTest()
        {
            var container = _container.Resolve<IContainer>();

            container.RegisterTransient<IThing, Thing>();

            Assert.Throws<InvalidOperationException>(() =>
            {
                container.RegisterTransient<IThing, Thing>();
            });
        }


        [Fact]
        public void RegisterRegisteredTypeWithRewritingTest()
        {
            var container = _container.Resolve<IContainer>();

            container.RegisterTransient<IThing, Thing>();
            container.RegisterTransient<IThing, Thing>(true);
        }


        [Fact]
        public void RegisterTypeWithWrongInheritanceTest()
        {
            var container = _container.Resolve<IContainer>();
            
            Assert.Throws<InvalidOperationException>(() =>
            {
                container.RegisterTransient(typeof(IThing), typeof(Part));
            });
        }


        [Fact]
        public void RegisterTypeWithWrongImplementationTest()
        {
            var container = _container.Resolve<IContainer>();
            
            Assert.Throws<NotSupportedException>(() =>
            {
                container.RegisterTransient<IThing, IThing>();
            });
        }


        [Fact]
        public void RegisterTypeWithMulipleConstructorsTest()
        {
            var container = _container.Resolve<IContainer>();
            
            Assert.Throws<NotSupportedException>(() =>
            {
                container.RegisterTransient<IPart, BadPart>();
            });
        }


        [Fact]
        public void ResolveUnregisteredTypeTest()
        {
            var container = _container.Resolve<IContainer>();
            
            Assert.Throws<NotSupportedException>(() =>
            {
                container.Resolve<IThing>();
            });
        }


        [Fact]
        public void ResolveTypeWithUnregisteredDependenciesTest()
        {
            var container = _container.Resolve<IContainer>();
            
            container.RegisterTransient<IThing, Thing>();

            Assert.Throws<NotSupportedException>(() =>
            {
                container.Resolve<IThing>();
            });
        }


        [Fact]
        public void GettingWrongInfoTest1()
        {
            var container = _container.Resolve<IContainer>();
            
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = container.IsRegistered(null);
            });
        }


        [Fact]
        public void GettingWrongInfoTest2()
        {
            var container = _container.Resolve<IContainer>();
            
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = container.GetImplementationType<IThing>();
            });
        }


        [Fact]
        public void GettingWrongInfoTest3()
        {
            var container = _container.Resolve<IContainer>();

            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = container.IsScoped<IThing>();
            });
        }


        [Fact]
        public void MainTest()
        {
            var container = _container.Resolve<IContainer>();
            
            container.RegisterTransient<IThing, Thing>();
            container.RegisterTransient<IPart, Part>();
            container.RegisterTransient<ISubPart, SubPart>();

            var subPart = container.Resolve<ISubPart>();

            Assert.IsType<SubPart>(subPart);
            Assert.Equal(typeof(SubPart), container.GetImplementationType<ISubPart>());

            var part = container.Resolve<IPart>();

            Assert.IsType<Part>(part);

            var thing = container.Resolve<IThing>();

            Assert.IsType<Thing>(thing);

            Assert.NotNull(thing.MainPart);
            Assert.NotNull(thing.FirstSubPart);
            Assert.NotNull(thing.SecondSubPart);
            Assert.NotNull(thing.MainPart.LeftSubPart);
            Assert.NotNull(thing.MainPart.RightSubPart);
        }
    }
}
