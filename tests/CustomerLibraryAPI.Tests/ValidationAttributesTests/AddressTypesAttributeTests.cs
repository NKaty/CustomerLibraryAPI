﻿using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.BusinessEntities.ValidationAttributes;
using Xunit;

namespace CustomerLibraryAPI.Tests.ValidationAttributesTests
{
    public class AddressTypesAttributeTests
    {
        [Fact]
        public void ShouldBeValid()
        {
            AddressTypesAttribute addressTypesAttribute = new AddressTypesAttribute();

            Assert.True(addressTypesAttribute.IsValid(AddressTypes.Shipping));
            Assert.True(addressTypesAttribute.IsValid(AddressTypes.Billing));
            Assert.True(addressTypesAttribute.IsValid(null));
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            AddressTypesAttribute addressTypesAttribute = new AddressTypesAttribute();

            Assert.False(addressTypesAttribute.IsValid("Shipping"));
            Assert.False(addressTypesAttribute.IsValid(""));
        }
    }
}