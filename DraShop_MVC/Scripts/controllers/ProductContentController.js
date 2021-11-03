/// <reference path="../angular.min.js" />
var app = angular.module("myApp", []);
app.controller('MyController', myController);
app.controller('product', productController);

// MyController
function myController($scope) {
    const user = {
        name: '',
        password: '',
        email: ''
    }
    $scope.user = user;
    $scope.login = () => {
        if ($scope.user.name === 'minhdra' && $scope.user.password === '1')
            alert('Successful!!!');
        else
            alert('Failure!!!');
    }
}

// ProductController
function productController($scope, $http) {
    $http({
        method: "get",
        url: "/Product/GetProducts"
    }).then(function success(res) {
        $scope.products = res.data.products
    }, function failure() { })
}