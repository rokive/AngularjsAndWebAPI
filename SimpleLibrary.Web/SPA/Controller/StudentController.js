var StudentController = function ($scope, $uibModal, Api) {

    
    $scope.studentId = "";
    $scope.fullName = "";
    $scope.fatherName = "";
    $scope.motherName = "";
    $scope.studentGender = "";
    $scope.religion = "";
    $scope.dOF = "";
    $scope.email = "";
    $scope.mobile = "";
    
    //$scope.studentList = {};

    function getSequence() {
        var str = localStorage.getItem("sequence").toString();
        if (str.length === 0) {
            Api.GetApiCall('Sequence', 'GetSequence', function (event) {
                if (event.hasErrors == true) {
                    alert("Error Getting data: " + event.error);
                } else {
                    localStorage.sequence = event.result;
                    $scope.studentId = event.result;
                }
            });
        }
        $scope.studentId = localStorage.getItem("sequence");
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
    getSequence();
    getALLStudent();

    $scope.save = function () {

            // check to make sure the form is completely valid
            if ($scope.student.$valid) {
                
                //var gender = document.getElementById("gender");
                //$scope.studentGender = gender.options[e.selectedIndex].value;
                //var religion = document.getElementById("religion");
                //$scope.religion = religion.options[e.selectedIndex].value;

                var student = {
                    studentId: $scope.studentId,
                    fullName: $scope.fullName,
                    fatherName: $scope.fatherName,
                    motherName: $scope.motherName,
                    studentGender: $scope.studentGender,
                    religion: $scope.religion,
                    dOF: $scope.dOF,
                    email: $scope.email,
                    mobile: $scope.mobile,
                    takenBook: 0
                }
                Api.PostApiCall('Student', 'SaveStudent', student, function (event) {
                    if (event.hasErrors == true) {
                        alert("Error save : " + event.error);
                    }
                    else {
                        alert(event.result);
                        $scope.studentId = "";
                        $scope.fullName = "";
                        $scope.fatherName = "";
                        $scope.motherName = "";
                        $scope.studentGender = "";
                        $scope.religion = "";
                        $scope.dOF = "";
                        $scope.email = "";
                        $scope.mobile = "";
                        localStorage.sequence = "";
                        getSequence();
                        getALLStudent();
                    }
                });
            }
            else {
                alert('Some thing is wrong!');
            }
        };
}

StudentController.$inject = ['$scope', '$uibModal', 'Api'];