using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Textaland.Controllers;
using Textaland.Models;
using Textaland.tests.Mocks;

namespace Textaland.tests.Controllers {
    [TestClass]
    public class ControllerTests {

        /*
         * First few tests is performed on a simple view since we have no prior
         * experience with unit tests simple to see that the connection
         * between the two projects is working and adjusting ourselves.
         */

        [TestMethod]
        public void FAQ() {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            ViewResult result = controller.FAQ() as ViewResult;

            //Assert
            Assert.AreEqual(true, result is ViewResult);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About() {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            ViewResult result = controller.About() as ViewResult;

            //Assert
            Assert.AreEqual(true, result is ViewResult);
            Assert.IsNotNull(result);
        }

        /*
         * Here we are testing to see if the NewTranslationRequest post
         * action does return the form data it recieves into the repo
         * if the form data it recieves is correct.
         * We will test the resulting view separately.
         */

        [TestMethod]
        public void TestNewTranslationRequestCorrect() {
            
            //Arrange

            List<TranslationRequest> translations = new List<TranslationRequest>();
            List<TranslationRequestUpvote> upvotes = new List<TranslationRequestUpvote>();

            var MockRequestRepo = new MockTranslatinoRequestRepo(translations);
            var MockUpvoteRepo = new MockTranslationRequestUpvoteRepo(upvotes);

            var controller = new TranslationRequestController(MockRequestRepo, MockUpvoteRepo);

            //Act

            var result = controller.NewTranslationRequest(GenerateCorrectForm());

            //Assert

            foreach (var item in MockRequestRepo.GetAllTranslationRequests())
	        {
                Assert.IsTrue(item._name == "Her");
	        }
            Assert.AreEqual(true, result is RedirectToRouteResult);
        }

        /*
         * Testing the view of all the requests, making sure that only 
         * 10 are visible per page and that the return is a ViewResult.
         */
        [TestMethod]
        public void TranslationRequestsViewWithCorrectNumberOfRequests10() {
            //Arrange

            List<TranslationRequest> translations = new List<TranslationRequest>();
            List<TranslationRequestUpvote> upvotes = new List<TranslationRequestUpvote>();

            for (int i = 0; i < 15; i++) {
                translations.Add(new TranslationRequest { _language = "ENG", _name = "Request" + i.ToString(), _numUpvotes = 0, Id = i, _userId = "someuser" + i.ToString() });
            }

            var MockRequestRepo = new MockTranslatinoRequestRepo(translations);
            var MockUpvoteRepo = new MockTranslationRequestUpvoteRepo(upvotes);

            var controller = new TranslationRequestController(MockRequestRepo, MockUpvoteRepo);

            //Act

            var result = controller.TranslationRequests(0) as ViewResult;

            //Assert

            Assert.IsTrue(Enumerable.Count(result.ViewBag.allRequests) == 10);
            Assert.AreEqual(true, result is ViewResult);

        }


        /*
         * Testing the view of all the requests, making sure that only 
         * 5 are visible per page and that the return is a ViewResult.
         */
        [TestMethod]
        public void TranslationRequestsViewWithCorrectNumberOfRequests5() {
            //Arrange

            List<TranslationRequest> translations = new List<TranslationRequest>();
            List<TranslationRequestUpvote> upvotes = new List<TranslationRequestUpvote>();

            for (int i = 0; i < 15; i++) {
                translations.Add(new TranslationRequest { _language = "ENG", _name = "Request" + i.ToString(), _numUpvotes = 0, Id = i, _userId = "someuser" + i.ToString() });
            }

            var MockRequestRepo = new MockTranslatinoRequestRepo(translations);
            var MockUpvoteRepo = new MockTranslationRequestUpvoteRepo(upvotes);

            var controller = new TranslationRequestController(MockRequestRepo, MockUpvoteRepo);

            //Act

            var result = controller.TranslationRequests(1) as ViewResult;

            //Assert

            Assert.IsTrue(Enumerable.Count(result.ViewBag.allRequests) == 5);
            Assert.AreEqual(true, result is ViewResult);

        }



        //Helper function to make some correct form data
        private static FormCollection GenerateCorrectForm() {
            FormCollection form = new FormCollection();
            form.Add("_name", "Her");
            form.Add("_language", "ENG");
            return form;
        }
    }
}
