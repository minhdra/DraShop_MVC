/// <reference path="../angular.min.js" />
/// <reference path="../jquery-3.6.0.min.js" />

"use Strict";
$(document).ready(function () {
    $('[data-tooltips="tooltip"]').tooltip();
});


const app = angular.module('AdminApp', ['ngFileUpload', 'angularUtils.directives.dirPagination']);

// Manage products
app.controller('ManageProducts', ManageProducts);
async function ManageProducts($rootScope, $scope, $http) {
    const urlGetProducts = '/Product/GetProducts';
    const urlGetProductsPagination = '/Product/GetProductsPagination';
    const urlGetCategories = '/Category/GetCategories';
    const urlDeleteProduct = '/Admin/ManageProducts/DeleteProduct';
    const urlAddProduct = '/Admin/ManageProducts/AddProduct';
    const urlUpdateProduct = '/Admin/ManageProducts/UpdateProduct';
    const urlUploadFiles = '/Admin/ManageProducts/Upload';
    const product = {
        _id: "",
        name: "",
        description: "",
        category_id: "1",
        made_in: "",
        gender: "",
        brand: "",
        style_id: "",
        status: "",
        image_avatar: "",
        summary: ""
    }
    $scope.product = product;

    // Sort
    $rootScope.sortColumn = '';
    $rootScope.reverse = true;
    $rootScope.direct = ''; // or Descending
    $scope.SortBy = () => {
        if ($scope.selectedSort) {
            const value = $scope.selectedValue;
            $rootScope.sortColumn = value;
            $rootScope.direct = $scope.selectedSort;
            if ($rootScope.direct === 'Ascending') {
                $rootScope.reverse = false;
                $rootScope.direct = 'Descending';
            }
            else if ($rootScope.direct === 'Descending') {
                $rootScope.reverse = true;
                $rootScope.direct = 'Ascending';
            }

        }
    }

    // pagination
    $scope.maxSize = 3;
    $rootScope.totalCount = 0;
    $scope.pageIndex = 1;
    $scope.pageSize = 10;
    $scope.keyword = '';
    const categorySelected = {id: ""};

    if (categorySelected) {
        $scope.selectedName = categorySelected.name;
        $scope.GetProuducts = async (index) => {
            //console.log(index);
            const urlPage = '/Product/GetProductsPagination';
            const options = {
                category_id: categorySelected.id.trim(),
                pageIndex: index,
                pageSize: $scope.pageSize,
                name: $scope.keyword
            }
            await $http({
                method: 'GET',
                url: urlGetProductsPagination,
                params: options
            }).then(response => {
                //console.log(response.data.listProducts);
                $scope.products = response.data.listProducts;
                $rootScope.totalCount = response.data.totalCount;
            }, reject => console.log(reject));
        }
        $scope.GetProuducts($scope.pageIndex);
    }
    else
        await $http({
        method: 'GET',
        url: urlGetProducts
    }).then(response => $scope.products = response.data.products)

    await $http({
        method: 'GET',
        url: urlGetCategories
    }).then(response => $rootScope.categories = response.data.categories);

    $scope.uploadFiles = (file) => {
        $scope.selectedFiles = file;
        if ($scope.selectedFiles && $scope.selectedFiles.length) {
            Upload.upload({
                url: urlUploadFiles,
                data: { files: $scope.selectedFiles, category_id: $scope.product.category_id }
            }).then(response => {
                $scope.product.image_avatar = d.data[0];
                console.log('Upload with status: ' + response.status);
            }, reject => console.log(reject));
        }
    }

    $scope.removeProduct = (item) => {
        $scope.selectedDelete = item;
    }

    $scope.confirmDelete = () => {
        $http({
            method: 'POST',
            url: urlDeleteProduct,
            params: { _id: $scope.selectedDelete._id.trim() }
        }).then(response => {
            const index = $scope.products.indexOf($scope.selectedDelete);
            $scope.products.splice(index, 1);
            console.log('Delete product with status: ' + response.status);
        }, reject => console.log(reject));
        $('#confirmForm').modal('hide');
    }

    $scope.addProduct = (event) => {
        product._id = GenerateProductId($scope.products);
        $scope.title = "Add Product";
        $scope.product = product;
        $scope.event = event;
    }

    $scope.editProduct = (item, event) => {
        $scope.title = "Update Product";
        $scope.product = angular.copy(item);
        $scope.event = event;
    };

    $scope.save = (item, event) => {
        if (event === 0) {
            $http({
                method: 'POST',
                url: urlAddProduct,
                data: { product: item }
            }).then(response => {
                $scope.products = [item, ...$scope.products];
                //console.log($scope.products);
                console.log('Add product with status: ' + response.status);
            }, reject => console.log(reject));
        }
        else {
            $http({
                method: 'POST',
                url: urlUpdateProduct,
                data: { product: item }
            }).then(response => {
                const index = $scope.products.findIndex(i => i._id.trim() === item._id.item());
                $scope.products[index] = item;
                console.log('Update product with status: ' + response.status);
            }, reject => console.log(reject));
        }
        $('#clickMe').modal('hide');
    }

    $scope.goDetails = function (event, item) {
        sessionStorage.setItem('productSelected', item._id.trim());
        const href = $(event.path[1]).data('href');
        window.location = href;
        //window.location = $(event.target).attr;
    }
}

// Products Details
app.controller('ManageProductDetails', ManageProductDetails);
async function ManageProductDetails($rootScope, $scope, $http) {
    // init
    const urlGetProductDetails = '/Detail/GetProduct';
    const urlGetProductColors = '/Admin/ManageProducts/GetProductColors';
    const urlAddColor = '/Admin/ManageProducts/AddProductColor';
    const urlUpdateColor = '/Admin/ManageProducts/UpdateProductColor';
    const urlDeleteColor = '/Admin/ManageProducts/DeleteProductColor';
    const urlAddSize = '/Admin/ManageProducts/AddProductSize';
    const urlUpdateSize = '/Admin/ManageProducts/UpdateProductSize';
    const urlDeleteSize = '/Admin/ManageProducts/DeleteProductSize';
    const urlAddPrice = '/Admin/ManageProducts/AddProductPrice';
    const urlUpdatePrice = '/Admin/ManageProducts/UpdateProductPrice';
    const urlDeletePrice = '/Admin/ManageProducts/DeleteProductPrice';
    $scope.index = 0;
    const product_id = sessionStorage.getItem('productSelected');
    const initColor = {
        _id: "",
        product_id: product_id,
        color: "",
        image: [],
        hex: "",
        ListSize: [],
    }
    $scope.color = initColor;
    $scope.listSize = ['XS', 'S', 'M', 'L', 'XL', '2XL', '3XL', '4XL'];
    // Get Detail
    await $http({
        method: 'GET',
        url: urlGetProductDetails,
        params: { id: product_id },
    }).then(response => {
        $scope.product = response.data;
        //console.log($scope.product);
        const colors = [];
        $scope.product.ListColor.forEach((item, index) => {
            colors.push({ _id: item._id.trim(), product_id: item.product_id, color: item.color, hex: item.hex, image: [], size: [] });
            item.image.split(', ').forEach(image => colors[index].image.push(image));
            item.ListSize.forEach(size => {
                colors[index].size.push({ _id: size._id, color_id: size.product_color_id, size: size.size, quantity: size.quantity });
            })
            //color[index].size.sort(compare);
        });

        $scope.colors = colors;
    }, reject => console.log(reject));

    // Get all colors
    const GetProductColors = async () => {
        await $http({
            method: 'GET',
            url: urlGetProductColors
        }).then(response => {
            $scope.productColors = response.data.colors;
            initColor._id = GenerateProductColorId($scope.productColors);
            console.log(initColor);
        })
    }

    // Set style
    $('#tableColors tbody tr:first').addClass('active');

    //
    $scope.showSize = (color_id) => {
        $scope.index = $scope.colors.findIndex(item => item._id.trim() === color_id.trim());
    }

    // Confirm image
    $scope.confirmImage = (link_img) => {
        $scope.color.image.push(link_img);
        $scope.colorImg = '';
    }

    // Remove image
    $scope.removeImage = (index) => {
        $scope.color.image.splice(index, 1);
    }

    // Add color
    $scope.addColor = async (event) => {
        $scope.ColorTitle = 'Add Color';
        $scope.color = initColor;
        await GetProductColors();
        $scope.event = event;
    }

    // Edit color
    $scope.editColor = (item, event) => {
        $scope.ColorTitle = 'Edit Color';
        $scope.color = angular.copy(item);
        $scope.event = event;
    }

    // Remove color
    $scope.removeColor = (item) => {
        $scope.selectedDelete = item;
    }
    $scope.confirmDelete = () => {
        $http({
            method: 'POST',
            url: urlDeleteColor,
            params: { _id: $scope.selectedDelete._id.trim() }
        }).then(response => {
            const index = $scope.colors.indexOf($scope.selectedDelete);
            $scope.colors.splice(index, 1);
            console.log('Delete color with status: ' + response.status);
        }, reject => console.log(reject));
        $('#confirmForm').modal('hide');
    }

    // Save color
    $scope.saveColor = async (item, event) => {
        item.hex = item.hex == "" ? "NaN" : item.hex;
        const tmp = angular.copy(item);
        tmp.image = tmp.image.join(', ');
        // Add color
        if (event === 0)
            await $http({
                method: 'POST',
                url: urlAddColor,
                data: { color: tmp },
            }).then(response => {
                $scope.colors = [item, ...$scope.colors];
                console.log('Add color with status: ' + response.status);
            },
                reject => console.log(reject));
        else
            await $http({
                method: 'POST',
                url: urlUpdateColor,
                data: { color: tmp },
            }).then(response => {
                $scope.colors.forEach(i => {
                    if (i._id.trim() == item._id.trim()) {
                        console.log(i, item);
                        i.hex = item.hex;
                        i.color = item.color;
                        i.image = item.image;
                        return;
                    }
                });
                $('.owl-carousel').owlCarousel('destroy');
                console.log('Update color with status: ' + response.status)
            },
                reject => console.log(reject));
        setupSlider();
        $('#ModalColor').modal('hide');
    }

    // Set up slider
    function setupSlider() {
        $('.owl-carousel').owlCarousel({
            autoplay: true,
            loop: true,
            responsiveClass: true,
            dots: false,
            nav: false,
            items: 1,
            singleItem: true,
            autoplayHoverPause: true,
        });
    };
    setupSlider();
    //await setTimeout(setupSlider, 500);
}

function GenerateProductId(products) {
    let ran = Math.floor(Math.random() * 100);
    let id = "P" + ran;
    for (let i = 0; i < products.length; i++) {
        if (products[i]._id.trim() === id) {
            id = "P" + ran;
            i--;
        }
    }
    return id;
}

function GenerateProductColorId(colors) {
    let ran = Math.floor(Math.random() * 100);
    let id = ran;
    //const check = colors.find(item => item._id.trim() == id);
    //while (check) {
    //    id = ran;
    //    check = colors.find(item => item._id.trim() == id);
    //}
    for (let i = 0; i < colors.length; i++) {
        if (colors[i]._id.trim() == id) {
            id = ran;
            i--;
        }
    }
    return id + '';
}