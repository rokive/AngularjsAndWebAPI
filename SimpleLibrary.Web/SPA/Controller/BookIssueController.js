var BookIssueController = function ($scope, $uibModal, Api) {
    $scope.issueQty = 0;
    $scope.totalIssueqty = 0;
    $scope.studentId = "";
    $scope.studentName = "";
    $scope.issueDate = "";
    $scope.isbn = "";
    $scope.title = "";
    $scope.author = "";
    $scope.publisher = "";

    $scope.books = [];

    $scope.studentIdChange = function() {
        $scope.studentName = "";
        $scope.books = [];
        $scope.totalIssueqty = 0;
    }

    $scope.isbnChange = function () {
        $scope.title = "";
        $scope.author = "";
        $scope.publisher = "";
    }

    $scope.addBook = function () {
        if ($scope.books.length < 2) {
            var book = {
                ISBN: $scope.isbn,
                BookTitle: $scope.title,
                AuthorName: $scope.author,
                PublisherName: $scope.publisher,
                Return: false,
                BookIssueMainId: 0
            };
            if (book.isbn !== "" && $scope.title !== "" && $scope.author !== "") {
                if ($scope.books.length === 0) {

                    $scope.books.push(book);
                    $scope.isbn = "";
                    $scope.title = "";
                    $scope.author = "";
                    $scope.publisher = "";
                }
                else {
                    var isExist = false;
                    var problem = '0';
                    for (var i = 0; i < $scope.books.length; i++) {
                        if ($scope.isbn === $scope.books[i].ISBN) {
                            problem = '1';
                        }
                    }
                    if ($scope.totalIssueqty + $scope.issueQty > 4) {
                        problem = '2';
                    }

                    switch (problem) {
                        case '1':
                            alert("This books already exist");
                            break;
                        case '2':
                            alert("Issue excess book");
                            break;
                        default:
                            $scope.books.push(book);
                            $scope.issueQty += 1;
                            $scope.isbn = "";
                            $scope.title = "";
                            $scope.author = "";
                            $scope.publisher = "";
                    }

                }
            }
            else {
                alert("Please Select Book");
            }
        }
        else {
            alert("only two books at a time issue");
        }
    };

    $scope.removeBook = function (index) {
        $scope.books.splice(index, 1);
        $scope.issueQty -= 1;
    };

    $scope.clearBook = function () {
        $scope.books = [];
    }

    $scope.findBook = function () {
        getALLBook();
        var isFind = false;
        for (var i = 0; i < $scope.bookList.length; i++) {
            if ($scope.isbn.toString() === $scope.bookList[i].ISBN) {
                $scope.title = $scope.bookList[i].BookTitle;
                $scope.author = $scope.bookList[i].AuthorName;
                $scope.publisher = $scope.bookList[i].PublisherName;
                isFind = true;
                break;
            }
        }
        if (!isFind) {
            alert("Books not found")
            $scope.title = "";
            $scope.author = "";
            $scope.publisher = "";
        }

    }
    $scope.findStudent = function () {
        getALLStudent();
        var isFind = false;
        for (var i = 0; i < $scope.studentList.length; i++) {
            if ($scope.studentId === $scope.studentList[i].StudentId) {
                $scope.studentName = $scope.studentList[i].FullName;
                $scope.totalIssueqty = $scope.studentList[i].TakenBook;
                isFind = true;
                break;
            }

        }
        if (!isFind) {
            alert("Student not found")
            $scope.totalIssueqty = 0;
            $scope.studentName = "";
        }

    }
    function getALLBook() {
        Api.GetApiCall('Book', 'GetAllBook', function (event) {
            if (event.hasErrors == true) {
                alert("Error getting data: " + event.error);
            } else {
                $scope.bookList = event.result;
            }
        });
    };
    function getALLStudent() {
        Api.GetApiCall('Student', 'GetAllStudent', function (event) {
            if (event.hasErrors == true) {
                alert("Error Getting data: " + event.error);
            } else {
                $scope.studentList = event.result;
            }
        });
    }
    getALLStudent();
    getALLBook();

    $scope.issue = function () {
        if ($scope.books.length === 0) {
            alert("Please add book")
            return;
        }
        bookIssue = {
            StudentId: $scope.studentId,
            IssueDate: $scope.issueDate,
            BookIssueDetails: $scope.books
        }
        Api.PostApiCall('BookIssue', 'BookIssueSave', bookIssue, function (event) {
            if (event.hasErrors == true) {
                alert("Error save : " + event.error);
            }
            else if (event.result !== "Save successfully") {
                alert(event.result);
            }
            else {
                alert(event.result);
                $scope.issueQty = 0;
                $scope.totalIssueqty = 0;
                $scope.studentId = "";
                $scope.studentName = "";
                $scope.issueDate = "";
                $scope.isbn = "";
                $scope.title = "";
                $scope.author = "";
                $scope.publisher = "";
                $scope.books = [];
            }
        });
    }
}

BookIssueController.$inject = ['$scope', '$uibModal', 'Api'];