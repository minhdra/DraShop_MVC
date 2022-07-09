/// <reference path="../angular.min.js" />
/// <reference path="../jquery-3.6.0.min.js" />

"use Strict";
$(document).ready(function () {
    $('[data-tooltips="tooltip"]').tooltip();
});


const app = angular.module('AdminApp', ['ngFileUpload', 'angularUtils.directives.dirPagination']);

const URL_GetOrders = '/Admin/ManageOrders/GetOrders';

const URL_GetAccountCustomer = '/Admin/ManageCustomers/GetCustomers';

// Home
app.controller('HomeController', HomeController);
function HomeController($scope, $http) {
    // Get new orders
    (function () {
        const newOrders = [];
        $http({
            method: 'GET',
            url: URL_GetOrders
        }).then(response => {
            response.data.forEach(item => {
                if (item.status == 0 && item.flag == 1)
                    newOrders.push(item);
            });
            $scope.newOrders = newOrders;
        }, reject => console.log(reject));
    })();
    // Get user registrations
    (function () {
        const userRegistrations = [];
        $http({
            method: 'GET',
            url: URL_GetAccountCustomer
        }).then(response => {
            response.data.forEach(item => {
                if (item.status == 0)
                    userRegistrations.push(item);
            });
            $scope.userRegistrations = userRegistrations;
        }, reject => console.log(reject));
    })();

    $scope.GoOrdersView = (status) => {
        sessionStorage.setItem('statusOrder', status);
    }
}

// Sidebar
app.controller('SidebarController', SidebarController);
function SidebarController($rootScope, $scope, $http) {
    const admin = JSON.parse(localStorage.getItem('admin'));
    const URL_SignOut = '/Admin/Admin/SignOut';

    $scope.admin = admin;
    $scope.SignOut = () => {
        sessionStorage.removeItem('admin');
        $http({
            method: 'POST',
            url: URL_SignOut
        }).then(response => console.log(response.status), reject => console.log(reject));
        GoLogin();
    }

    function GoLogin() {
        function disableBack() { window.history.forward(); }
        setTimeout(disableBack, 0);
        window.onunload = function () { null };
        window.location.href = '/Admin/Admin/Login';
    }

    $scope.GoOrdersView = (status) => {
        sessionStorage.setItem('statusOrder', status);
    }
}

// Manage products
app.controller('ManageProductsController', ManageProductsController);
async function ManageProductsController($rootScope, $scope, $http) {
    const urlGetProducts = '/Product/GetProducts';
    const urlGetProductsPagination = '/Product/GetProductsPagination';
    const urlGetCategories = '/Category/GetCategories';
    const urlDeleteProduct = '/Admin/ManageProducts/DeleteProduct';
    const urlAddProduct = '/Admin/ManageProducts/AddProduct';
    const urlUpdateProduct = '/Admin/ManageProducts/UpdateProduct';
    const urlUploadFiles = '/Admin/ManageProducts/Upload';
    const product = {
        _id: "Auto after add",
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
        $scope.GetProducts = async (index) => {
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
        $scope.GetProducts($scope.pageIndex);
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

    $scope.removeProduct = (e, item) => {
        e.stopPropagation();
        $scope.selectedDelete = item;
        $('#confirmForm').modal('show');
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
        $scope.title = "Add Product";
        $scope.product = product;
        $scope.event = event;
    }

    $scope.editProduct = (e, item, event) => {
        e.stopPropagation();
        $scope.title = "Update Product";
        $scope.product = angular.copy(item);
        $scope.event = event;
        $('#clickMe').modal('show');
    };

    $scope.save = (item, event) => {
        if (event === 0) {
            $http({
                method: 'POST',
                url: urlAddProduct,
                data: { product: item }
            }).then(response => {
                const p = response.data;
                $scope.products = [p, ...$scope.products];
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
app.controller('ManageProductDetailsController', ManageProductDetailsController);
async function ManageProductDetailsController($rootScope, $scope, $http) {
    // init
    const urlGetProductDetails = '/Detail/GetProduct';
    const urlGetProductColors = '/Admin/ManageProducts/GetProductColors';
    const urlGetProductPrices = '/Admin/ManageProducts/GetProductPrices';
    const urlGetProductSizes = '/Admin/ManageProducts/GetProductSizes';
    const urlAddColor = '/Admin/ManageProducts/AddProductColor';
    const urlUpdateColor = '/Admin/ManageProducts/UpdateProductColor';
    const urlDeleteColor = '/Admin/ManageProducts/DeleteProductColor';
    const urlAddSize = '/Admin/ManageProducts/AddSize';
    const urlUpdateSize = '/Admin/ManageProducts/UpdateSize';
    const urlDeleteSize = '/Admin/ManageProducts/DeleteSize';
    const urlAddPrice = '/Admin/ManageProducts/AddPrice';
    const urlUpdatePrice = '/Admin/ManageProducts/UpdatePrice';
    const urlDeletePrice = '/Admin/ManageProducts/DeletePrice';
    $scope.index = 0;
    const product_id = sessionStorage.getItem('productSelected');
    const initColor = {
        _id: "Auto after add",
        product_id: product_id,
        color: "",
        image: [],
        hex: "",
        size: [],
    };
    const initSize = {
        _id: "Auto after add",
        product_color_id: "",
        size: "XS",
        quantity: 1,
    };
    const initPrice = {
        _id: "Auto after add",
        product_id: product_id,
        price_current: 0,
        date_effect: "",
        date_expired: "",
    }
    const listSize = [
        { size: 'XS', status: '' },
        { size: 'S', status: '' },
        { size: 'M', status: '' },
        { size: 'L', status: '' },
        { size: 'XL', status: '' },
        { size: '2XL', status: '' },
        { size: '3XL', status: '' },
        { size: '4XL', status: '' }];
    $scope.color = initColor;
    $scope.size = initSize;
    $scope.listSize = listSize;
    // Get Detail
    await $http({
        method: 'GET',
        url: urlGetProductDetails,
        params: { id: product_id },
    }).then(response => {
        $scope.product = response.data;
        const colors = [];
        $scope.product.ListColor.forEach((item, index) => {
            colors.push({ _id: item._id.trim(), product_id: item.product_id, color: item.color, hex: item.hex, image: [], size: [] });
            item.image.split(', ').forEach(image => colors[index].image.push(image));
            item.ListSize.forEach(size => {
                colors[index].size.push({ _id: size._id, product_color_id: size.product_color_id, size: size.size, quantity: size.quantity });
            })
            //color[index].size.sort(compare);
        });
            $scope.colors = colors;
        if (colors.length > 0) {
            $scope.size.product_color_id = colors[0]._id;
        }
    }, reject => console.log(reject));
    // Get all

    // Set style
    $('#tableColors tbody tr:first').addClass('active');

    //
    $scope.showSize = (color, e) => {
        $('#tableColors tbody tr').removeClass('active');
        $(e.path[1]).addClass('active');
        $scope.index = $scope.colors.findIndex(item => item._id.trim() === color._id.trim());
        $scope.size.product_color_id = color._id;
    }

    // Confirm image
    $scope.confirmImage = (link_img) => {
        if ($scope.color.image.indexOf(link_img) === -1) {
            $scope.color.image.push(link_img);

        }
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
        //await GetProductColors();
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
        $scope.nameDelete = 'color';
    }
    $scope.confirmDelete = () => {
        if ($scope.nameDelete === 'color')
            $http({
                method: 'POST',
                url: urlDeleteColor,
                params: { _id: $scope.selectedDelete._id.trim() }
            }).then(response => {
                const index = $scope.colors.indexOf($scope.selectedDelete);
                $scope.colors.splice(index, 1);
                console.log('Deleted with status: ' + response.status);
            }, reject => console.log(reject));
        else if ($scope.nameDelete === 'size')
            $http({
                method: 'POST',
                url: urlDeleteSize,
                params: { _id: $scope.selectedDelete._id.trim() }
            }).then(response => {
                $scope.colors.forEach(i => {
                    if (i._id.trim() === $scope.selectedDelete.product_color_id.trim()) {
                        const index = i.size.indexOf($scope.selectedDelete);
                        i.size.splice(index, 1);
                        return;
                    }
                })
                
                console.log('Deleted with status: ' + response.status);
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
                const c = response.data;
                c.image = item.image;
                $scope.colors.unshift(c);
                //console.log('Add color with status: ' + response.status);
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
                        i.hex = item.hex;
                        i.color = item.color;
                        i.image = item.image;
                        return;
                    }
                });
                $('.owl-carousel').owlCarousel('destroy');
                //console.log('Update color with status: ' + response.status)
            },
                reject => console.log(reject));
        await setupSlider();
        $('#ModalColor').modal('hide');
    }

    // Add size
    $scope.addSize = async (event) => {
        $scope.SizeTitle = 'Add Size';
        $scope.size = initSize;
        //await GetProductSizes();
        $scope.event = event;
        //checkSize();
    }

    // Edit size
    $scope.editSize = (item, event) => {
        $scope.SizeTitle = 'Edit Size';
        //checkSize(item);
        $scope.size = angular.copy(item);
        $scope.event = event;
    }

    // Delete size
    $scope.removeSize = (item) => {
        $scope.selectedDelete = item;
        $scope.nameDelete = 'size';
    }

    // Save size
    $scope.saveSize = async (item, event) => {
        const tmp = angular.copy(item);
        // Add size
        if (event === 0) {
            const indexColor = $scope.colors.findIndex(value => value._id === tmp.product_color_id);
            const indexSize = $scope.colors[indexColor].size.findIndex(value => value.size === tmp.size);
            if (indexSize >= 0) {
                tmp._id = $scope.colors[indexColor].size[indexSize]._id;
                tmp.quantity += $scope.colors[indexColor].size[indexSize].quantity;
                await $http({
                    method: 'POST',
                    url: urlUpdateSize,
                    data: { size: tmp },
                }).then(response => {
                    $scope.colors[indexColor].size[indexSize].quantity = tmp.quantity;
                    console.log('Update color with status: ' + response.status)
                }, reject => console.log(reject));
            }
            else {
                await $http({
                    method: 'POST',
                    url: urlAddSize,
                    data: { size: tmp },
                }).then(response => {
                    const s = response.data;
                    $scope.colors.forEach(i => {
                        if (i._id.trim() === s.product_color_id.trim()) {

                            i.size = [s, ...i.size];
                            return;
                        }
                    });
                    //$scope.colors.size = [item, ...$scope.colors.size];
                    console.log('Add size with status: ' + response.status);
                },
                    reject => console.log(reject));
            }
        }
        else
            await $http({
                method: 'POST',
                url: urlUpdateSize,
                data: { size: tmp },
            }).then(response => {
                $scope.colors.forEach(i => {
                    if (i._id.trim() === tmp.product_color_id.trim()) {
                        i.size.forEach(j => {
                            if (j._id.trim() == tmp._id.trim()) {
                                j.size = tmp.size;
                                j.quantity = tmp.quantity;
                                return;
                            }
                        });
                        return;
                    }
                })
                console.log('Update color with status: ' + response.status)
            }, reject => console.log(reject));

        $('#ModalSize').modal('hide');
    }
    // Check size
    const checkSize = (i) => {
        $scope.listSize = listSize;
        console.log($scope.colors[$scope.index]);
        $scope.colors[$scope.index].size.forEach(item => {
            const index = $scope.listSize.findIndex(s => s.size == item.size);
            //console.log(index, $scope.listSize)
            if (index !== -1)
                $scope.listSize[index].status = 'disabled';
        })

        if (i) {
            const index = $scope.listSize.findIndex(s => s.size == i.size);
            $scope.listSize[index].status = '';
        }
        //console.log($scope.listSize);
        //angular.element(document).ready(function () {
        //    $('#ModalSize option').prop('disabled', false);
        //    $('#ModalSize option.disabled').prop('disabled', true);
        //})
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

// Login 
app.controller('LoginController', LoginController);
function LoginController($rootScope, $scope, $http) {
    // Css
    (function () {
        const formGroup = $('.form-group');
        $scope.ChangeUsername = (username) => {
            if (username.trim() === "") $(formGroup[0]).removeClass('field--not-empty');
            else $(formGroup[0]).addClass('field--not-empty');
        }

        $scope.ChangePassword = (password) => {
        if (password.trim() === "") $(formGroup[1]).removeClass('field--not-empty');
        else $(formGroup[1]).addClass('field--not-empty');
    }
    })();

    const URL_GetAdmin = '/Admin/Admin/GetAdmin';

    $scope.Login = (username, password) => {
        $http({
            method: 'POST',
            url: URL_GetAdmin,
            params: { username, password },
        }).then(response => {
            // Login fail
            $rootScope.admin = response.data;
            if (response.data == "") {
                // Remove success and show error alert
                $('.alert-success').removeClass('show').hide();
                $('.alert-danger').addClass('show').show();
                // Set value for alert
                $scope.failure = 'Username or password not correct!';
                // Hide alert after time
                setTimeout(() => $('.alert-danger').removeClass('show').hide(), 3000);
            }
            // Login success
            else {
                // Remove error and show success alert
                $('.alert-danger').removeClass('show').hide();
                $('.alert-success').addClass('show').show();
                // Declare variable customer and set local storage
                //$rootScope.admin = response.data;
                localStorage.setItem('admin', JSON.stringify($scope.admin));
                // Set value for alert
                $scope.success = 'Login success.';
                // Set time


                // Hide alert after time
                setTimeout(() => {
                    $('.alert-success').removeClass('show').hide();
                    window.location.href = '/Admin/HomeAdmin/Index';
                }, 2000);
            }
        }, reject => console.log(reject));
    }
}

// Manage orders
app.controller('ManageOrdersController', ManageOrdersController);
function ManageOrdersController($scope, $rootScope, $http) {
    const URL_UpdateOrder = '/Admin/ManageOrders/UpdateOrder';
    let details = [], orders = [], ordersCanceled = [];
    const statusOrder = sessionStorage.getItem('statusOrder') | '';
    $scope.filter = statusOrder.toString();
    // Sort
    $scope.sortColumn = '';
    $scope.reverse = true;
    $scope.direct = ''; // or Descending
    $scope.SortBy = () => {
        if ($scope.selectedSort) {
            const value = $scope.selectedValue;
            $scope.sortColumn = value;
            $scope.direct = $scope.selectedSort;
            if ($scope.direct === 'Ascending') {
                $scope.reverse = false;
                $scope.direct = 'Descending';
            }
            else if ($scope.direct === 'Descending') {
                $scope.reverse = true;
                $scope.direct = 'Ascending';
            }

        }
    }
    // Get orders
    $scope.filterByStatus = function (filter) {
        $http({
            method: 'GET',
            url: URL_GetOrders
        }).then(response => {
            orders = [];
            ordersCanceled = [];
            response.data.forEach(order => {
                if (order.status == 5 && order.flag == 1)
                    ordersCanceled.push(order);
                if ((filter == '' || filter == order.status) && order.flag == 1)
                    orders.push(order);
            })
            $scope.orders = orders;
            $scope.ordersCanceled = ordersCanceled;
        }, reject => console.log(reject));
    }; $scope.filterByStatus($scope.filter);

    // Get order when click
    $scope.getStatus = (order) => {
        $scope.order = angular.copy(order);
    }
    // Change status of orders
    $scope.confirmSwitch = (option) => {
        if (option == 0) {
            if ($scope.order.status != 0)
                $scope.order.status = $scope.order.status - 1;
        }
        else if (option == 1) {
            if ($scope.order.status != 3)
                $scope.order.status = $scope.order.status + 1;
        }
        else if (option == 2)
            $scope.order.status = 5;
        else
            $scope.order.flag = 0;
        $http({
            method: 'POST',
            url: URL_UpdateOrder,
            data: { order: $scope.order }
        }).then(response => {
            const index = $scope.orders.findIndex(item => item._id === $scope.order._id);
            if (option != 3)
                $scope.orders[index].status = $scope.order.status;
            else {
                $scope.ordersCanceled.push($scope.orders[index]);
                $scope.orders.splice(index, 1);
            }

            $scope.filterByStatus($scope.filter);
            $('#confirmModal').modal('hide'); $('#confirmRemove').modal('hide');
        }, reject => console.log(reject));
    }
    // Go order details view
    $scope.goDetails = (order_id) => {
        $rootScope.order_id = order_id;
        sessionStorage.setItem('order_id', order_id);
        location.href = '/Admin/ManageOrders/ManageOrderDetailsView';
    }


}

// Manage order details
app.controller('ManageOrderDetailsController', ManageOrderDetailsController);
function ManageOrderDetailsController($rootScope, $scope, $http) {
    const URL_GetOrderDetails = '/Admin/ManageOrders/GetOrderDetails';
    const URL_UpdateOrderDetail = '/Admin/ManageOrders/UpdateOrderDetail';
    const order_id = sessionStorage.getItem('order_id');

    $scope.getOrderDetails = () => {
        $http({
            method: 'GET',
            url: URL_GetOrderDetails,
            params: { order_id }
        }).then(response => {
            $scope.orderDetails = [];
            response.data.forEach(item => {
                if (item.flag == 1)
                    $scope.orderDetails.push(item);
            })
            //console.log(response.data)
        }, reject => console.log(reject));
    }; $scope.getOrderDetails();

    $scope.editOrderDetail = (detail) => {
        $scope.detail = angular.copy(detail);
        $scope.getFieldsProduct(detail.product_id, detail.color, detail.size);
    }
    $scope.removeOrderDetail = (detail) => $scope.detail = angular.copy(detail);


    $scope.getFieldsProduct = (product_id, color, size) => {
        $scope.images = [];
        $scope.colors = [];
        $scope.sizes = [];
        const index = $scope.orderDetails.findIndex(item => item.product_id === product_id);
        $scope.orderDetails[index].product.ListColor.forEach(item => {
            $scope.colors.push(item.color);
            if (color === item.color) {
                const tmp = item.image.split(', ');
                $scope.images = [...$scope.images, ...tmp];
                item.ListSize.forEach(s => {
                    if (size === s.size) $scope.maxQuantity = s.quantity;
                    if (s.quantity > 0)
                        $scope.sizes.push(s);
                });
            }
            
        });
    }

    $scope.save = (detail, flag) => {
        if (flag == 0) detail.flag = flag;
        $http({
            method: 'POST',
            url: URL_UpdateOrderDetail,
            data: { detail }
        }).then(response => {
            const index = $scope.orderDetails.findIndex(item => item._id === detail._id);
            if (flag == 0)
                $scope.orderDetails.splice(index, 1);
            else
                $scope.orderDetails[index] = detail;
        }, reject => console.log(reject));

        $('#updateModal').modal('hide');
        $('#confirmModal').modal('hide');
    }
}