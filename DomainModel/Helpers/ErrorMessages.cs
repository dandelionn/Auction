//-----------------------------------------------------------------------
// <copyright file="ErrorMessages.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DomainModel.Validators
{
    /// <summary>
    /// Defines the .<see cref="ErrorMessages" />.
    /// </summary>
    public static class ErrorMessages
    {
        /// <summary>
        /// Defines the NameRequired.
        /// </summary>
        public const string NameRequired = "Name is required";

        /// <summary>
        /// Defines the SurnameRequired.
        /// </summary>
        public const string SurnameRequired = "Surname is required";

        /// <summary>
        /// Defines the LengthBetween2And50.
        /// </summary>
        public const string LengthBetween2And50 = "The length must be between 2 and 50";

        /// <summary>
        /// Defines the ProductsRequired.
        /// </summary>
        public const string ProductsRequired = "Products is required";

        /// <summary>
        /// Defines the ProductRequired.
        /// </summary>
        public const string ProductRequired = "Product is required";

        /// <summary>
        /// Defines the PersonRequired.
        /// </summary>
        public const string PersonRequired = "Person is required";

        /// <summary>
        /// Defines the ValueRequired.
        /// </summary>
        public const string ValueRequired = "Value is required";

        /// <summary>
        /// Defines the BidsRequired.
        /// </summary>
        public const string BidsRequired = "Bids is required";

        /// <summary>
        /// Defines the CategoriesRequired.
        /// </summary>
        public const string CategoriesRequired = "Categories Is required";

        /// <summary>
        /// Defines the NegativePrice.
        /// </summary>
        public const string NegativePrice = "Price is Negative";

        /// <summary>
        /// Defines the NegativeValue.
        /// </summary>
        public const string NegativeValue = "Value is Negative";

        /// <summary>
        /// Defines the UserRequired.
        /// </summary>
        public const string UserRequired = "User is required";

        /// <summary>
        /// Defines the AuctionRequired.
        /// </summary>
        public const string AuctionRequired = "Auction is required";

        /// <summary>
        /// Defines the PriceRequired.
        /// </summary>
        public const string BeginPriceRequired = "Price is required";

        /// <summary>
        /// Defines the UsersRequired.
        /// </summary>
        public const string UsersRequired = "Users is required";

        /// <summary>
        /// Defines the BooksRequired.
        /// </summary>
        public const string BooksRequired = "Books is required";

        /// <summary>
        /// Defines the TitleRequired.
        /// </summary>
        public const string TitleRequired = "Title is required";

        /// <summary>
        /// Defines the DomainsRequired.
        /// </summary>
        public const string DomainsRequired = "Domains is required";

        /// <summary>
        /// Defines the AuthorsRequired.
        /// </summary>
        public const string AuthorsRequired = "Authors is required";

        /// <summary>
        /// Defines the LengthBetween10And13.
        /// </summary>
        public const string LengthBetween10And13 = "The length must be between 10 and 13";

        /// <summary>
        /// Defines the OnlyNumbersAllowed.
        /// </summary>
        public const string OnlyNumbersAllowed = "Only numbers are allowed";

        /// <summary>
        /// Defines the TypeRequired.
        /// </summary>
        public const string TypeRequired = "Type is required";

        /// <summary>
        /// Defines the LengthGreaterThanZero.
        /// </summary>
        public const string LengthMustBeGreaterThanZero = "The length must be greater than 0";

        /// <summary>
        /// Defines the BookRequired.
        /// </summary>
        public const string BookRequired = "Book is required";

        /// <summary>
        /// Defines the PublisherRequired.
        /// </summary>
        public const string PublisherRequired = "Publisher is required";

        /// <summary>
        /// Defines the LoanDateRequired.
        /// </summary>
        public const string LoanDateRequired = "Loan date is required";

        /// <summary>
        /// Defines the DateNotValid.
        /// </summary>
        public const string InvalidDate = "Date is not valid";

        /// <summary>
        /// Defines the ReturnDateRequired.
        /// </summary>
        public const string ReturnDateRequired = "Return date is required";

        /// <summary>
        /// Defines the EditionRequired.
        /// </summary>
        public const string EditionRequired = "Edition is required";

        /// <summary>
        /// Defines the ReaderRequired.
        /// </summary>
        public const string ReaderRequired = "Reader is required";

        /// <summary>
        /// Defines the NotAValidPhoneNumber.
        /// </summary>
        public const string InvalidPhoneNumber = "Not a valid phone number";

        /// <summary>
        /// Defines the LengthBetween2And100.
        /// </summary>
        public const string LengthBetween2And100 = "The length must be between 2 and 100";

        /// <summary>
        /// Defines the NotAValidEmailAddress.
        /// </summary>
        public const string InvalidEmailAddress = "Not a valid email address";

        /// <summary>
        /// Defines the NormalOrPersonnelReader.
        /// </summary>
        public const string NormalOrPersonnelReader = "Reader type should be normal or personnel";

        /// <summary>
        /// Defines the ReturnDateLaterOrEqualLoanDate.
        /// </summary>
        public const string ReturnDateLaterOrEqualLoanDate = "Return date should be later than or equal to loan date";

        /// <summary>
        /// Defines the ActualReturnDateLaterOrEqualLoanDate.
        /// </summary>
        public const string ActualReturnDateLaterOrEqualLoanDate = "Actual return date should be later than or equal to loan date";

        /// <summary>
        /// Defines the PhoneOrEmailRequired.
        /// </summary>
        public const string PhoneOrEmailRequired = "Either phone or email is required";

        /// <summary>
        /// Defines the AtLeastOneElementInCollection.
        /// </summary>
        public const string AtLeastOneElementInCollection = "List should contain at least one element";

        /// <summary>
        /// Defines the LanguageRequired.
        /// </summary>
        public const string LanguageRequired = "Language is required";

        /// <summary>
        /// Defines the EditionsRequired.
        /// </summary>
        public const string EditionsRequired = "Editions is required";

        /// <summary>
        /// Defines the PhoneRequired.
        /// </summary>
        public const string PhoneRequired = "Phone number is required";

        /// <summary>
        /// Defines the LoansRequired.
        /// </summary>
        public const string LoansRequired = "Loans is required";

        /// <summary>
        /// Defines the BookCountRequired.
        /// </summary>
        public const string BookCountRequired = "Book count is required";

        /// <summary>
        /// Defines the BookCountGreaterOrEqualToZero.
        /// </summary>
        public const string BookCountGreaterOrEqualToZero = "Book count should be greater or equal to 0";

        /// <summary>
        /// Defines the PageCountRequired.
        /// </summary>
        public const string PageCountRequired = "Page count is required";

        /// <summary>
        /// Defines the ReadingRoomBookCountRequired.
        /// </summary>
        public const string ReadingRoomBookCountRequired = "Reading room book count is required";

        /// <summary>
        /// Defines the LessOrEqualToBookCount.
        /// </summary>
        public const string LessOrEqualToBookCount = "Reading room book count should be less or equal to book count";

        /// <summary>
        /// Defines the ReadingRoomBookCountGreaterOrEqualToZero.
        /// </summary>
        public const string ReadingRoomBookCountGreaterOrEqualToZero = "Reading room book count should be greater or equal to 0";

        /// <summary>
        /// Defines the IsForReadingRoomRequired.
        /// </summary>
        public const string IsForReadingRoomRequired = "Is for reading room question is required";

        /// <summary>
        /// Defines the YesOrNo.
        /// </summary>
        public const string YesOrNo = "It should be yes or no";

        /// <summary>
        /// Defines the NotEnoughAvailableEditionsForLoan.
        /// </summary>
        public const string NotEnoughAvailableEditionsForLoan = "Not available editions of the book for loan!";

        /// <summary>
        /// Defines the TooManyBooksLoanedInTheLastPeriod.
        /// </summary>
        public const string TooManyBooksLoanedInTheLastPeriod = "Too many books loaned int the last period";

        /// <summary>
        /// Defines the TooManyBooksFromTheSameDomain.
        /// </summary>
        public const string TooManyBooksFromTheSameDomain = "Too many books from the same domain";

        /// <summary>
        /// Defines the AddressRequired.
        /// </summary>
        public const string AddressRequired = "Address is required";

        /// <summary>
        /// Defines the ParentCategoriesRequired.
        /// </summary>
        public const string ParentCategoriesRequired = "ParentCategories is required";

        /// <summary>
        /// Defines the LengthNotValid.
        /// </summary>
        public const string LengthNotValid = "Length is not valid";

        /// <summary>
        /// Defines the AuctionsRequired.
        /// </summary>
        public const string AuctionsRequired = "Auctions is required";

        /// <summary>
        /// Defines the BeginDateRequired.
        /// </summary>
        public const string BeginDateRequired = "BeginDate is required";

        /// <summary>
        /// Defines the EndDateRequired.
        /// </summary>
        public const string EndDateRequired = "EndDate is required";

        /// <summary>
        /// Defines the BeginDateIsAfterEndDate.
        /// </summary>
        public const string BeginDateIsAfterEndDate = "BeginDate is after EndDate";

        /// <summary>
        /// Defines the EndDateIsBeforeBeginDate.
        /// </summary>
        public const string EndDateIsBeforeBeginDate = "EndDate is before BeginDate";

        /// <summary>
        /// Defines the AuctionPeriodIsTooLarge.
        /// </summary>
        public const string AuctionPeriodIsTooLarge = "Auction period is too large";

        /// <summary>
        /// Defines the BeginDateShouldNotBeInThePast.
        /// </summary>
        public const string BeginDateShouldNotBeInThePast = "BeginDate should not be in the past";

        /// <summary>
        /// Defines the CurrencyTypeRequired.
        /// </summary>
        public const string CurrencyNameRequired = "CurrencyType is required";

        public const string CurrencyNameIsNotValid = "Currency name is not valid";

        public const string TooSmallAuctionBeginPrice = "The auction begin price is too small";

        public const string CurrentPriceRequired = "CurrentPrice is required";

        public const string OwnedAuctionsRequired = "OwnedAuctions is required";

        public const string SellerRequired = "Seller is required";

        public const string BidderRequired = "Bidder is required";

        public const string SellersRequired = "Sellers is required";

        public const string AuctionPriceTooSmall = "Current auction price is to small";

        public const string UsernameRequired = "Username is required";

        public const string PasswordRequired = "Password is required";

        public const string EmailRequired = "Email is required";

        public const string PhoneNumberRequired = "Phone number is required";
    }
}
