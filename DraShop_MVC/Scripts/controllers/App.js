/// <reference path='../angular.min.js' />
/// <reference path="../jquery-3.6.0.min.js" />
"use strict";


// Header element
const header = $('header');
// get height header
const heightHeader = header.outerHeight();
const sticky = 'sticky';
$('.introduce').css('padding-top', heightHeader);
AOS.init();
$(document).ready(function () {
    $('[data-tooltips="tooltip"]').tooltip();
});
const ApiCity = '/Models/DataCity.json';


const app = angular.module('app', ['angularUtils.directives.dirPagination']);

app.controller('HomeController', HomeController)
function HomeController($scope) {
    const getClassBody = sessionStorage.getItem('classBody');
    if (!getClassBody) $scope.classBody = 'home';
    else $scope.classBody = getClassBody;
}

// Menu
app.controller('MenuController', MenuController);
async function MenuController($scope, $rootScope, $http) {
    const url = '/Category/GetCategories';

    await $http({
        method: 'GET',
        url
    })
        .then(
            response => $rootScope.categories = response.data.categories,
            reject => console.log(reject)
    );

    $scope.selectCategory = (event, id, name, gender) =>
    {
        const dropItems = $('header .dropdown-item');
        dropItems.removeClass('active');
        $(event).addClass('active');
        localStorage.setItem('selectedCate', JSON.stringify({ id, name, gender }));
    };

    $scope.toggleCart = () => {
        $('.cart__wrap').addClass('show');
        $('.cart__overlay').addClass('show');
        $('body').css('overflow', 'hidden');
    }

    $scope.signOut = () => {
        localStorage.removeItem('customer');
        window.location.reload();
    }

    //
    $scope.cancelClick = (e) => {
        e.stopPropagation();
        e.preventDefault();
    }

    
    // change class 
    function changeClass() {
            const navItems = $('header .nav-item');
            navItems.click(function () {
                    const text = $(this).find('.nav-link').text().trim().toLowerCase();
                    sessionStorage.setItem('classBody', text);
            });
    }
    changeClass();

    //
    const customer = JSON.parse(localStorage.getItem('customer'));
    if (customer) {
        $('.user__wrap .dropdown').removeClass('active');
        $('.user__wrap .profile').addClass('active');
        $scope.customer = customer;
    }
    else {
        $('.user__wrap .dropdown').removeClass('active');
        $('.user__wrap .user').addClass('active');
    }
}

// Get Product
app.controller('ProductController', ProductController);
async function ProductController($rootScope, $scope, $http) {
    // Set padding
    const main = $('.products');
    main.css('padding-top', heightHeader);
    header.addClass('hasBg');

    // init Sort
    $rootScope.sortColumn = '_id';
    $rootScope.reverse = true;
    $rootScope.direct = 'Ascending'; // or Descending
    // Handle sort
    $scope.SortBy = () => {
        const value = $scope.valueSort.split('|');
        $rootScope.sortColumn = value[0];
        $rootScope.direct = value[1];
        if ($rootScope.direct === 'Ascending') {
            $rootScope.reverse = false;
            $rootScope.direct = 'Descending';
        }
        else {
            $rootScope.reverse = true;
            $rootScope.direct = 'Ascending';
        }
    }

    // Get items
    // init pagination
    $scope.maxSize = 3;
    $rootScope.totalCount = 0;
    $scope.pageIndex = 1;
    $scope.pageSize = 9;
    $scope.keyword = '';
    // get selected category
    const categorySelected = JSON.parse(localStorage.getItem("selectedCate"));
    const urlPage = '/Product/GetProductsByCategoryAndGender';
    if (categorySelected) {
        $scope.selectedName = categorySelected.name;
        // Handle get products
        $scope.GetProducts = async (index) => {
            // Set params
            const options = {
                category_id: categorySelected.id.trim(),
                gender: categorySelected.gender,
                pageIndex: index,
                pageSize: $scope.pageSize,
                name: $scope.keyword
            }
            await $http({
                method: 'GET',
                url: urlPage,
                params: options
            }).then(response => {
                //console.log(response.data);
                
                $scope.products = response.data.listProducts;
                $rootScope.totalCount = response.data.totalCount;
            }, reject => console.log(reject));
        }
        $scope.GetProducts($scope.pageIndex);
    }
    else {
        // Get all
        const urlGetAll = '/Product/GetProducts';
        await $http({
            method: 'GET',
            url: urlGetAll
        })
            .then(
                response => $scope.products = response.data.products,
                reject => console.log(reject)
            );
    }
    

    // Get by category
    //const urlGetByCate = '/Product/GetProductsByCategory';
    //console.log($rootScope.selectedCateId);
    //const category_id = categorySelected.id;
    //await $http({
    //    method: 'GET',
    //    url: urlGetByCate,
    //    params: { category_id }
    //})
    //    .then(response => $scope.products_cate = response.data.products,
    //        reject => console.log(reject));
    //console.log($scope.products_cate);

    $rootScope.goDetail = (id) =>
        sessionStorage.setItem('selectedProduct', id);
}

// Cart
app.controller('CartController', CartController);
async function CartController($rootScope, $scope, $http) {
    // init
    $rootScope.totalItems = 0;
    $rootScope.total = 0;
    const cartContent = $('.cart__content');
    const customer = JSON.parse(localStorage.getItem('customer'));
    let listCart;
    //console.log(customer);
    // function
    $scope.close = () => {
        $('.cart__wrap').removeClass('show');
        $('.cart__overlay').removeClass('show');
        $('body').css('overflow', 'auto');
    }

    const urlGetCarts = '/Cart/GetCart';

    // Handle get cart of the customer
    if (customer) {
        await $http({
            method: 'GET',
            url: urlGetCarts,
            params: { customer_id: customer._id.trim() }
        }).then(response => {
            //console.log(response.data);
            $rootScope.cart = response.data;
            //console.log($rootScope.cart, response.data);
            if (response.data.ListCartDetail.length > 0) {
                listCart =  $rootScope.cart.ListCartDetail;
                $rootScope.totalItems = listCart.length;
                $rootScope.cart.ListCartDetail.forEach(item => {
                    item.status = item.status == 1 ? true : false;
                    if (item.status)
                        $rootScope.total += item.price * item.quantity;
                })
            }
        });
    }

    // Handle update cart
    $scope.calcTotal = async function (index, item, quantity) {
        const cartD = angular.copy(item);
        //console.log(cartD, item);
        cartD.status = cartD.status == true ? 1 : 0;
        //cartD.quantity = quantity;
        const urlUpdate = '/Cart/UpdateCartDetail';
        await $http({
            method: 'POST',
            url: urlUpdate,
            data: { cartDetail: cartD }
        }).then(response => {
            $rootScope.total = 0;
            //const index = $rootScope.cart.ListCartDetail.findIndex(i => i._id == cartD._id);
            $rootScope.cart.ListCartDetail[index].quantity = quantity;
            $rootScope.cart.ListCartDetail[index].size = cartD.size;
            $rootScope.cart.ListCartDetail.forEach(item => {
                if (item.status)
                    $rootScope.total += item.price * item.quantity;
            })
        });
    };

    // Handle remove product in cart
    $scope.remove = function ($event, item) {
        const cartD = angular.copy(item);
        const urlDelete = '/Cart/DeleteCartDetail';
        //$($event.currentTarget).parents('li').remove();
        $http({
            method: 'POST',
            url: urlDelete,
            params: { _id: cartD._id.trim() },
        }).then(response => {
            const index = $rootScope.cart.ListCartDetail.findIndex(i => i._id = cartD._id);
            $rootScope.cart.ListCartDetail.splice(index, 1);
            $rootScope.total -= cartD.price * cartD.quantity;
            $rootScope.totalItems -= 1;
            if (!customer || $rootScope.cart.ListCartDetail.length == 0) {
                cartContent.removeClass('active');
                $(cartContent.get(0)).addClass('active');
            }
        });
    };

    // Set css when has product or not
    if (!customer || $rootScope.cart.ListCartDetail.length == 0) {
        cartContent.removeClass('active');
        $(cartContent.get(0)).addClass('active');
    }
    else {
        cartContent.removeClass('active');
        $(cartContent.get(1)).addClass('active');
    }

}

// Slideshow
app.controller('SlideShowController', SlideShowController);
async function SlideShowController($scope, $http) {
    angular.element(document).ready(function () {
        $('.slideshow').css('margin-top', heightHeader);
        $('.slideshow').css('height', `calc(100vh - ${heightHeader}px)`);
    })
    $scope.ResetCategory = () => {
        localStorage.removeItem('selectedCate');
    }

    // Set up slider
    function setupSlider() {
        $('.slideshow .owl-carousel').owlCarousel({
            //autoplay: true,
            loop: true,
            responsiveClass: true,
            dots: false,
            nav: true,
            items: 1,
            singleItem: true,
            autoplayHoverPause: true,
        });
    };
    setupSlider();
    //await setTimeout(setupSlider, 500);

    // Handle video
    const videoSlider = $('.owl-carousel');
    videoSlider.on('translate.owl.carousel', function (e) {
        $('.owl-item video').each(function () {
            // pause playing video - after sliding
            $(this).get(0).pause();
        });
    });
    videoSlider.on('translated.owl.carousel', function (e) {
        // check: does the slide have a video?
        if ($('.owl-item.active').find('video').length !== 0) {
            // play video in active slide
            $('.owl-item.active video').get(0).play();
            //$(this).click(function () {
            //    if ($('.owl-item.active video').get(0).paused == false) {
            //        $('.owl-item.active video').get(0).play();
            //        $('.owl-carousel').trigger('stop.owl.autoplay');
            //    } else {
            //        $('.owl-item.active video').get(0).pause();
            //        $('.owl-carousel').trigger('play.owl.autoplay');
            //    }
            //})
            $('.owl-carousel').trigger('stop.owl.autoplay');
        }
    });
}

// Client Slider
app.controller('ClientSlideController', ClientSlideController);
function ClientSlideController($rootScope, $scope, $http) {
    angular.element(document).ready(async function () {
        // init
        const urlBestSelling = '/Product/GetProductsBestSelling';
        const urlNew = '/Product/GetProductsNew';
        const urlBestDiscount = '/Product/GetProductsBestDiscount';
        let length = 9;

        // Get products best selling
        await $http({
            method: 'GET',
            url: urlBestSelling,
            params: {length}
        }).then(response => {
            $scope.productsHotMen = [];
            $scope.productsHotWomen = [];
            response.data.products.forEach(item => {
                if (item.gender == 0)
                    $scope.productsHotMen.push(item);
                else
                    $scope.productsHotWomen.push(item);
            })
        },
            reject => console.log(reject));

        // Get products new
        await $http({
            method: 'GET',
            url: urlNew,
            params: { length }
        }).then(response => {
            $scope.productsNewMen = [];
            $scope.productsNewWomen = [];
            //console.log(response.data.products);
            response.data.products.forEach(item => {
                if (item.gender == 0)
                    $scope.productsNewMen.push(item);
                else
                    $scope.productsNewWomen.push(item);
            })
        },
            reject => console.log(reject));

        // Get products discount
        await $http({
            method: 'GET',
            url: urlBestDiscount,
            params: { length }
        }).then(response => {
            $scope.productsDiscountMen = [];
            $scope.productsDiscountWomen = [];
            response.data.products.forEach(item => {
                if (item.gender == 0)
                    $scope.productsDiscountMen.push(item);
                else
                    $scope.productsDiscountWomen.push(item);
            })
        },
            reject => console.log(reject));

        // Setup slider
        function setupSlider() {
            $('.products__wrapper.owl-carousel').owlCarousel({
                margin: 10,
                //autoplay: true,
                autoHeight: true,
                loop: true,
                responsiveClass: true,
                video: true,
                responsive: {
                    300: {
                        items: 1
                    },
                    600: {
                        items: 2
                    },
                    1000: {
                        items: 4
                    }
                }
            });
        };
        //setupSlider();
        await setTimeout(setupSlider, 500);

        // Go to detail
        $rootScope.goDetail = (id) =>
            sessionStorage.setItem('selectedProduct', id);
        //console.log($scope.productsMen);

        
    })
}

// Detail page
app.controller('DetailController', DetailController);
function DetailController($rootScope, $scope, $http) {
    angular.element(document).ready(async function () {
        // init
        const customer = JSON.parse(localStorage.getItem('customer'));
        $scope.index = 0;
        $scope.indexSize = 0;
        const cartContent = $('.cart__content');
        const urlAddToCart = '/Cart/AddToCart';

        // Handle change color
        $scope.changeColor = async (index, color) => {
            if (index === $scope.index) return;
            $scope.selectedColor = color;
            //console.log($scope.indexSize);
            $('.list-color > div').removeClass('active');
            $(`.list-color > div:nth-child(${index + 1})`).addClass('active');
            $('.owl-carousel').owlCarousel('destroy');
            $scope.index = index;
            $('.product__visual__list').addClass('owl-carousel');
            console.log($scope.indexSize);
            setTimeout(() => {
                setupSlider()
                $(`.product__details-size li:nth-child(${$scope.indexSize + 1})`).addClass('active');
            }, 250);
            checkStock();
        }
        // Handle change size
        $scope.changeSize = (index, quantity, size) => {
            $scope.selectedQuantity = quantity;
            $scope.selectedSize = size;
            index === $scope.indexSize ? null : $scope.indexSize = index;
            $('.product__details-size li').removeClass('active');
            $(`.product__details-size li:nth-child(${index + 1})`).addClass('active');
            checkStock();
        }

        // Handle add cart
        // start
        $scope.addToCart = async (product_id, price, image, name) => {
            // If customer not login, will go to login page
            if (!customer) window.location.href = '/Customer/Login';
            // Product not in stock
            if ($scope.selectedQuantity === 0) {
                //do something 
            }
            // Product in stock
            else {
                // Get date now
                const date = new Date().toJSON().slice(0, 10).replace(/-/g, '/');
                // Cart data
                const cart = { _id: "", customer_id: customer._id };
                const cartDetail = {
                    _id: "",
                    cart_id: "",
                    product_id,
                    quantity: 1,
                    price,
                    date_create: date,
                    status: 1,
                    size: $scope.selectedSize,
                    color: $scope.selectedColor,
                    image,
                    name,
                }
                // Post cart
                await $http({
                    method: 'POST',
                    url: urlAddToCart,
                    data: { cart, cartDetail }
                }).then(response => {
                    $rootScope.total = 0;

                    // Find index of cart detail
                    const indexCheck = $rootScope.cart.ListCartDetail.findIndex(i => i._id == response.data._id);

                    // Not found
                    if (indexCheck === -1) {
                        // Add product in interface
                        $rootScope.cart.ListCartDetail = [response.data, ...$rootScope.cart.ListCartDetail];
                        // Increasing the total list
                        $rootScope.totalItems += 1;
                    }
                    // Found
                    else {
                        //console.log($rootScope.cart.ListCartDetail[indexCheck]);
                        // If found, update new info
                        $rootScope.cart.ListCartDetail[indexCheck].quantity = response.data.quantity;
                        $rootScope.cart.ListCartDetail[indexCheck].size = response.data.size;
                        $rootScope.cart.ListCartDetail[indexCheck].image = response.data.image;
                    }

                    // Calulate total price again!
                    $rootScope.cart.ListCartDetail.forEach(item => {
                        $rootScope.total += item.price * item.quantity;
                    })

                    // Show cart has products, instead of no products
                    if (!customer || $rootScope.cart.ListCartDetail.length == 0) {
                        cartContent.removeClass('active');
                        $(cartContent.get(0)).addClass('active');
                    }
                    else {

                        cartContent.removeClass('active');
                        $(cartContent.get(1)).addClass('active');
                    }
                }, reject => console.log(reject));
            }
        }
        // end

        // Set padding, css
        // Need padding for main content
        const main = $('.product__details__wrapper');
        main.css('padding-top', heightHeader);
        header.addClass('hasBg');
        
        // Set up slider
        function setupSlider() {
            $('.product__visual__list.owl-carousel').owlCarousel({
                autoplay: true,
                loop: true,
                responsiveClass: true,
                dots: false,
                nav: true,
                items: 1,
                autoplayHoverPause: true,
            });
        };
        //setupSlider();
        //await setTimeout(setupSlider, 500);

        // Get Product
        // start
        // Get id of selected product 
        const id = sessionStorage.getItem('selectedProduct');
        // Get detail of the product
        await $http({
            url: '/Detail/GetProduct/',
            params: { id },
            method: 'GET'
        }).then(response => {
            // Declare
            $scope.product = response.data;
            const color = [];
            // Handle response data
            $scope.product.ListColor.forEach((item, index) => {
                // Add color
                color.push({ color: item.color, hex: item.hex, image: [], size: [] });
                // Split string and add to array
                item.image.split(', ').forEach(image => color[index].image.push(image));
                item.ListSize.forEach(size => {
                    color[index].size.push({ size: size.size, quantity: size.quantity });
                })
                // Sort size
                color[index].size.sort(compare);
            })
            
            $scope.color = { color_list: color };
        }, reject => console.log(reject));
        // Set slider
        setupSlider();
        // func compare
        function compare(a, b) {
            if (a.size < b.size) {
                return -1;
            }
            if (a.size > b.size) {
                return 1;
            }
            return 0;
        }

        // init
        const colorElement = $('.list-color > div');
        const sizeElement = $('.product__details-size li');
        colorElement.first().addClass('active');
        sizeElement.first().addClass('active');

        // Check produc in stock
        function checkStock() {
            let size;
            // Get size is activing
            $('.product__details-size li').each(function (index, item) {
                if ($(item).hasClass('active')) {
                    size = item.innerText;
                }
            })
            // Loop
            $scope.color.color_list[$scope.index].size.forEach(item => {
                if (item.size === size) {
                    // In stock
                    if (item.quantity > 0) {
                        $('.stock').removeClass('active');
                        $('.stock.in__stock').addClass('active');
                    } else { // Not in stock
                        $('.stock').removeClass('active');
                        $('.stock.not__in__stock').addClass('active');
                    }
                }
            })
        }
        checkStock();

        // init
        $scope.selectedColor = $scope.color.color_list[0].color;
        $scope.selectedSize = $scope.color.color_list[0].size[0].size;
        $scope.selectedQuantity = $scope.color.color_list[0].size[0].quantity;
       
    })
    
}

// Register Customer
app.controller('RegisterController', RegisterController);
async function RegisterController($scope, $http) {
    // init
    const input = $('.validate-input .input100');
    const urlRegister = '/Customer/SignUp';
    // Handle Register
    $scope.Register = async function (username, email, password) {
        let check = true;

        for (let i = 0; i < input.length; i++) {
            if (validate(input[i]) == false) {
                showValidate(input[i]);
                check = false;
            }
        }
        if (check) {
            
            await $http({
                method: 'POST',
                url: urlRegister,
                dataType: 'json',
                data: { username, email, password },
            }).then(
                response => {
                    // Register success
                    if (response.data.customer) {
                        // Remove error and show success alert
                        $('.alert-danger').removeClass('show').hide();
                        $('.alert-success').addClass('show').show();
                        // Declare variable customer
                        $scope.customer = response.data.customer;
                        // Set value for alert
                        $scope.success = 'You register successful.';
                        // Hide alert after time
                        setTimeout(() => $('.alert-success').removeClass('show').hide(), 3000);
                    }
                    // Register fail
                    else {
                        // Remove success and show error alert
                        $('.alert-success').removeClass('show').hide();
                        $('.alert-danger').addClass('show').show();
                        // Set value for alert
                        $scope.failure = 'Your username already exists!';
                        // Hide alert after time
                        setTimeout(() => $('.alert-danger').removeClass('show').hide(), 3000);
                    }
                },
                reject => console.log(reject));
        }
    }
}

// Login
app.controller('LoginController', LoginController);
function LoginController($rootScope, $scope, $http) {
    // Init
    $scope.close = "";
    $scope.login = 0;
    $rootScope.remember = false;
    const urlLogin = '/Customer/GetCustomer';
    const input = $('.validate-input .input100');

    // Handle login
    $scope.Login = async function (username, password, remember) {
        let check = true;
        // Validator
        for (let i = 0; i < input.length; i++) {
            if (validate(input[i]) == false) {
                showValidate(input[i]);
                check = false;
            }
        }
        if (check) {
            await $http({
                method: 'GET',
                url: urlLogin,
                params: { username, password, remember },
            })
                .then(async response => {
                    // Login fail
                    if (response.data.login == "0") {
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
                        $scope.customer = response.data.customer;
                        localStorage.setItem('customer', JSON.stringify($scope.customer));
                        // Set value for alert
                        $scope.success = 'Login success.';
                        // Set time


                        // Hide alert after time
                        await setTimeout(() => {
                            $('.alert-success').removeClass('show').hide();
                            window.open('/', '_parent');
                        }, 2000);
                    }
                },
                reject => console.log(reject));
        }
    }
}

// Payment
app.controller('PaymentController', PaymentController);
async function PaymentController($rootScope, $scope, $http) {
    // init
    const urlAddOrder = '/Order/AddOrder';
    const urlAddAddress = '/Customer/CreateDeliveryAddress';
    const urlGetAddresses = '/Customer/GetDeliveryAddresses';
    const urlUpdateAddress = '/Customer/UpdateDeliveryAddress';
    const urlDeleteAddress = '/Customer/DeleteDeliverAddress';
    $scope.firstNone = 'd-none';
    $scope.secondNone = 'd-flex';
    $scope.totalPrice = 0;
    $scope.transportFee = 2;
    const order = {
        _id: "",
        date_create: "",
        address: "",
        total: 0,
        status: 0,
        customer_id: "",
        orderDetails: [],
    }
    const orderDetail = {
        _id: "",
        order_id: "",
        product_id: "",
        quantity: 0,
        price: 0,
        image: '',
    }
    const orderDetails = [];
    const address = {
        _id: '',
        customer_id: '',
        customer_name: '',
        phone_number: '',
        province: '',
        district: '',
        communce: '',
        specific_address: '',
        type_address: '',
        status: 0
    };
    const customer = JSON.parse(localStorage.getItem('customer'));
    //$scope.listAddress = customer.deliveryAddresses;
    //console.log($scope.listAddress);

    //$.getJSON(ApiCity, function (data) {
    //    console.log(data);
    //})

    // toggle address
    $scope.ChangeAddress = () => {
        $scope.firstNone = 'd-flex';
        $scope.secondNone = 'd-none';
    }
    const GetAddresses = () => {
        $http({
            method: 'GET',
            url: urlGetAddresses,
            params: { customer_id: customer._id.trim() }
        }).then(response => {
            $scope.listAddress = response.data;
            // Set status
            $scope.listAddress.forEach(item => {
                if (item.status == 1) {
                    $scope.address = item;
                }
                //item.status = item.status == 1 ? true : false;
            })  
        }, reject => console.log(reject));
    }
    GetAddresses();

    // Get all city of Viet Nam
    $.getJSON(ApiCity, function (data) {
        $scope.ListProvince = data;
    })
    // Handle chose city
    $scope.ChoseAddress = (index) => {
        if (index === 0)
            $scope.ListProvince.forEach(item => {
                if (item.Id === $scope.SelectedProvince) {
                    $scope.address.province = item.Name;
                    $scope.ListDistrict = item.Districts;
                    return;
                }
            })
        else 
            $scope.ListDistrict.forEach(item => {
                if (item.Id === $scope.SelectedDistrict) {
                    $scope.address.district = item.Name;
                    $scope.ListCommune = item.Wards;
                    return;
                }
            })
    }

    $scope.AddAddress = () => {
        $scope.title = 'Add New Delivery Address';
        $scope.address = address;
    }
    $scope.ConfirmCreateAddress = (item, isDefault) => {
        const address = angular.copy(item);
        const type_address = $('.type__address button.active').text();

        address.customer_id = customer._id;
        address.type_address = type_address;
        address.status = isDefault ? 1 : 0;

        $http({
            method: 'POST',
            url: urlAddAddress,
            data: { deliveryAddress: address }
        }).then(response => {
            if (response.data.status == 1) $scope.address = response.data;
            $scope.listAddress.push(response.data);
            customer.deliveryAddress.push(response.data);
            localStorage.setItem('customer', JSON.stringify(customer));
            //console.log(response.data);
        })


        $('#ModalAddAddress').modal('hide');
    }
    $scope.ConfirmAddress = () => {
        const list = $('.list__address li');
        const listItemCheck = $('.list__address li input[name="address"]:checked');
        //console.log(list, listItemCheck, JSON.parse(listItemCheck.val()));
        const selectedAddress = JSON.parse(listItemCheck.val());
        selectedAddress.status = 1;

        $scope.listAddress.forEach(item => {
            if (item._id !== selectedAddress._id)
                item.status = 0;
            else
                item.status = selectedAddress.status;
        })

        $scope.address = selectedAddress;

        $scope.firstNone = 'd-none';
        $scope.secondNone = 'd-flex';
        
    }
    $scope.CancelChange = () => {
        $scope.firstNone = 'd-none';
        $scope.secondNone = 'd-flex';
    }
    $scope.confirmPurchase = () => {
        const addressCus = $('#addressCus').text();
        order.address = addressCus.replace('  ', ' ').trim();
        const date = new Date().toJSON().slice(0, 10).replace(/-/g, '/');
        order.date_create = date;
        order.customer_id = customer._id;
        order.total = $rootScope.total + $scope.transportFee;

        $rootScope.cart.ListCartDetail.forEach(item => {
            if (item.status) {
                orderDetail.product_id = item.product_id;
                orderDetail.quantity = item.quantity;
                orderDetail.price = item.price;
                orderDetail.image = item.image;
                orderDetails.push(orderDetail);
            }  
        })
        //console.log(order, orderDetails);
        $http({
            method: 'POST',
            url: urlAddOrder,
            data: { order, orderDetails }
        }).then(response => {
            //console.log(response.data.order);
            customer.order = response.data.order;
            localStorage.setItem('customer', JSON.stringify(customer));
        }, reject => console.log(reject));

        $('#ModalWarning').modal('hide');
    }
}

// Order's customer
app.controller('OrderCustomerController', OrderCustomerController);
function OrderCustomerController($scope, $http) {
    // init
    const urlGetOrder = '/Order/GetOrder';
    const customer = JSON.parse(localStorage.getItem('customer'));
    $scope.customer = customer;
    const handleGet = () => {
        $http({
            method: 'GET',
            url: urlGetOrder,
            params: { customer_id: customer._id }
        }).then(response => {
            const orders = response.data;
            $scope.orders0 = [];
            $scope.orders1 = [];
            $scope.orders2 = [];
            $scope.orders3 = [];
            $scope.orders4 = [];
            orders.forEach(order => {
                //console.log(order)
                if (order.status == 0) $scope.orders0.push(order);
                else if (order.status == 1) $scope.orders1.push(order);
                else if (order.status == 2) $scope.orders2.push(order);
                else if (order.status == 3) $scope.orders3.push(order);
                else $scope.orders4.push(order);
            });
            //console.log($scope.orders0)
        }, reject => console.log(reject));
    }
    handleGet();
}


/*==================================================================
[ Focus Contact2 ]*/
$('.input100').each(function () {
    $(this).on('blur', function () {
        if ($(this).val().trim() != "") {
            $(this).addClass('has-val');
        }
        else {
            $(this).removeClass('has-val');
        }
    })
})

/*==================================================================
[ Validate ]*/
$('.validate-form .input100').each(function () {
    $(this).focus(function () {
        hideValidate(this);
    });
});

function validate(input) {
    if ($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
        if ($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
            return false;
        }
    }
    else if ($(input).attr('name') == 'pass-confirm') {
        const pass = $('input[name="pass"]').val().trim();
        if (pass !== $(input).val().trim() || $(input).val().trim() == '') return false;
    }
    else {
        if ($(input).val().trim() == '') {
            return false;
        }
    }
}

function showValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).addClass('alert-validate');
}

function hideValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).removeClass('alert-validate');
}

$('.close').on('click', function () {
    $(this).parent().removeClass('show').hide();
});


function checkExpiration() {
    //check if past expiration date
    var value = localStorage.getItem('timeOut');
    console.log(value);
    //check "my hour" index here
    if (value < new Date()) {
        localStorage.removeItem("customer");
    }
}

function myFunction() {
    var myinterval = 15 * 60 * 1000; // 15 min interval
    setInterval(function () { checkExpiration(); }, 2000);
}

function GoBack() {
    history.back();
}

//myFunction();