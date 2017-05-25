var BookController = function ($scope, $uibModal, Api) {
    $scope.isbn = "";
    $scope.bookTitle = "";
    $scope.authorName = "";
    $scope.publisherName = "";

    function getALLBook() {
        Api.GetApiCall('Book', 'GetAllBook', function (event) {
            if (event.hasErrors == true) {
                alert("Error getting data: " + event.error);
            } else {
                $scope.bookList = event.result;
            }
        });
    };

    function existingBook() {
        var isExist = false;
        for (var i = 0; i < $scope.bookList.length; i++) {
            if ($scope.isbn.toString() === $scope.bookList[i].ISBN) {
                isExist = true
                break;
            }
        }
        return isExist;
    }
    $scope.save = function () {
        if ($scope.book.$valid && !existingBook()) {
            var book = {
                ISBN: $scope.isbn,
                BookTitle: $scope.bookTitle,
                AuthorName: $scope.authorName,
                PublisherName: $scope.publisherName
            }
            Api.PostApiCall('Book', 'BookSave', book, function (event) {
                if (event.hasErrors == true) {
                    alert("Error save : " + event.error);
                }
                else if (event.result !== "Save successfully") {
                    alert(event.result);
                }
                else {
                    alert(event.result)
                    $scope.isbn = "";
                    $scope.bookTitle = "";
                    $scope.authorName = "";
                    $scope.publisherName = "";
                    getALLBook();
                }
            });
        }
        else {
            alert('Please Enter the valid data');
        }
    };
    getALLBook();
}

BookController.$inject = ['$scope', '$uibModal', 'Api'];