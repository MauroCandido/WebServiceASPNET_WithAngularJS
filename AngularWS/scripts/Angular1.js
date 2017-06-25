// Create Module
var app = angular.module("ProductsApp", []);

// Create Controle

app.controller("ProductsCtrl", function ($scope, $http) {
    $http.get('ProductsService.asmx/getProducts')
    .then(function (response) {
        $scope.products = response.data;
    }
    );
}
);

