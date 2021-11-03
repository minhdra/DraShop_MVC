﻿/// <reference path='../angular.min.js' />
/// <reference path="../../content/owlcarousel2-2.3.4/dist/owl.carousel.js" />
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


const app = angular.module('app', ['angularUtils.directives.dirPagination']);

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

    $scope.selectCategory = (id, name) =>
        { localStorage.setItem('selectedCate', JSON.stringify({ id, name })) };
}

// Get Product
app.controller('ProductController', ProductController);
async function ProductController($rootScope, $scope, $http) {
    // Set padding
    const main = $('.products');
    main.css('padding-top', heightHeader);
    header.addClass('hasBg');

    // Sort
    $rootScope.sortColumn = '';
    $rootScope.reverse = true;
    $rootScope.direct = 'Ascending'; // or Descending
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
    // pagination
    $scope.maxSize = 3;
    $scope.totalCount = 0;
    $scope.pageIndex = 1;
    $scope.pageSize = 9;
    $scope.keyword = '';
    
    const categorySelected = JSON.parse(localStorage.getItem("selectedCate"));
    if (categorySelected) {
        $scope.selectedName = categorySelected.name;
        $scope.GetProuducts = async (index) => {
            console.log(index);
            const urlPage = '/Product/GetProductsPagination';
            const options = {
                category_id: categorySelected.id.trim(),
                pageIndex: index,
                pageSize: $scope.pageSize,
                name: $scope.keyword
            }
            await $http({
                method: 'GET',
                url: urlPage,
                params: options
            }).then(response => {
                console.log(response.data.listProducts);
                $scope.products = response.data.listProducts;
                $scope.totalCount = response.data.totalCount;
            }, reject => console.log(reject));
        }
        $scope.GetProuducts($scope.pageIndex);
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
    const urlGetByCate = '/Product/GetProductsByCategory';
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
}

// Client Slider
app.controller('ClientSlideController', ClientSlideController);
function ClientSlideController($rootScope, $scope, $http) {
    angular.element(document).ready(async function () {
        
        const urlMen = '/Product/GetProductsHotMen';
        const urlWomen = '/Product/GetProductsHotWomen';

        // Get hot products of men
        await $http({
            method: 'GET',
            url: urlMen
        }).then(response => $scope.productsMen = response.data.products,
            reject => console.log(reject));

        // Get hot products of women
        await $http({
            method: 'GET',
            url: urlWomen
        }).then(response => $scope.productsWomen = response.data.products,
            reject => console.log(reject));

        function setupSlider() {
            console.log('a');
            $('.products__wrapper.owl-carousel').owlCarousel({
                margin: 20,
                autoplay: true,
                autoHeight: true,
                loop: true,
                responsiveClass: true,
                responsive: {
                    300: {
                        items: 1
                    },
                    600: {
                        items: 2
                    },
                    1000: {
                        items: 3
                    }
                }
            });
        }; 
        await setTimeout(setupSlider, 500);

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
        $scope.index = 0;
        $scope.indexSize = 0;
        $scope.clickMenuColor = () => {
            
        }
        $scope.changeColor = async (index) => {
            console.log($scope.indexSize);
            if (index === $scope.index) return;
            $('.list-color > div').removeClass('active');
            $(`.list-color > div:nth-child(${index + 1})`).addClass('active');
            $('.slick-list').remove();
            $('.slick-slider').slick('unslick');
            $scope.index = index;
            $('.product__visual__list').addClass('slick-slider');
            await setTimeout(setupSlider, 500);
            $(`.product__details-size li:nth-child(${$scope.indexSize + 1})`).addClass('active');
            checkStock();
            //setupSlider()
        }
        $scope.changeSize = (index, quantity) => {
            index === $scope.indexSize ? null : $scope.indexSize = index;
            $('.product__details-size li').removeClass('active');
            $(`.product__details-size li:nth-child(${index + 1})`).addClass('active');
            checkStock();
        }
        // Set padding, css
        const main = $('.product__details__wrapper');
        main.css('padding-top', heightHeader);
        header.addClass('hasBg');
        
        // Config slider for visual
        function setupSlider() {
            jQuery('.slick-slider').not('.slick-initialized').slick({
                infinite: true,
                autoplay: true,
                //dots: true,
                slidesToShow: 1,
                slidesToScroll: 1,
                arrows: true,
                prevArrow: $('.btn-slide-prev'),
                nextArrow: $('.btn-slide-next'),
                speed: 500,
            })
        };
        await setTimeout(setupSlider, 500);

        // Get Product
        const id = sessionStorage.getItem('selectedProduct');
        await $http({
            url: '/Detail/GetProduct/',
            params: { id },
            method: 'get'
        }).then(response => {
            $scope.product = response.data.products[0];
            const color = [];
            $scope.product.ListColor.forEach((item, index) => {
                color.push({ color: item.color, hex: item.hex, image: [], size: [] });
                item.image.split(', ').forEach(image => color[index].image.push(image));
                item.ListSize.forEach(size => {
                    color[index].size.push({ size: size.size, quantity: size.quantity });
                })
                color[index].size.sort(compare);
            })
            
            $scope.color = { color_list: color };
        }, reject => console.log(reject));

        function compare(a, b) {
            if (a.size < b.size) {
                return -1;
            }
            if (a.size > b.size) {
                return 1;
            }
            return 0;
        }


        const colorElement = $('.list-color > div');
        const sizeElement = $('.product__details-size li');
        colorElement.first().addClass('active');
        sizeElement.first().addClass('active');
        //console.log($scope.color.color_list[$scope.index].size);
        function checkStock() {
            let size;
            $('.product__details-size li').each(function (index, item) {
                if ($(item).hasClass('active')) {
                    size = item.innerText;
                }
            })
            $scope.color.color_list[$scope.index].size.forEach(item => {
                if (item.size === size) {
                    if (item.quantity > 0) {
                        $('.stock').removeClass('active');
                        $('.stock.in__stock').addClass('active');
                    } else {
                        $('.stock').removeClass('active');
                        $('.stock.not__in__stock').addClass('active');
                    }
                }
            })
        }
        checkStock();
        

        //console.log($scope.color);
        //console.log($scope.color.color_list[0].image);
        //console.log($scope.color.color_list);
    })
    
}

// Register Customer
app.controller('RegisterController', RegisterController);
async function RegisterController($scope, $http) {
    $scope.Register = async function (username, email, password) {
        var input = $('.validate-input .input100');
        let check = true;

        for (var i = 0; i < input.length; i++) {
            if (validate(input[i]) == false) {
                showValidate(input[i]);
                check = false;
            }
        }
        if (check) {
            const url = '/Customer/SignUp';
            await $http({
                method: 'POST',
                url,
                dataType: 'json',
                data: { username, email, password },
            }).then(
                response => {
                    //$('.alert').removeClass('show');
                    if (response.data.customer) {
                        $('.alert-success').addClass('show').show();
                        $scope.customer = response.data.customer;
                        $scope.success = 'You register successful.'
                    }
                    else {
                        $('.alert-danger').addClass('show').show();
                        $scope.failure = 'Your username already exists!';
                    }
                },
                reject => console.log(reject));
        }
    }
}

// Login
app.controller('LoginController', LoginController);
function LoginController($scope, $http) {
    $scope.Login = async function (username, password) {
        var input = $('.validate-input .input100');
        let check = true;

        for (var i = 0; i < input.length; i++) {
            if (validate(input[i]) == false) {
                showValidate(input[i]);
                check = false;
            }
        }
        if (check) {
            const url = '/Customer/GetCustomer';
            await $http({
                method: 'Get',
                url,
                dataType: 'json',
                params: { username, password },
            })
                .then(async response => {
                    if (response.data.customer) {
                        $('.alert-success').addClass('show').show();
                        $scope.customer = response.data.customer;
                        $scope.success = 'Login success.';
                        localStorage.setItem('customer', JSON.stringify($scope.customer));
                        await setTimeout(() => {
                            $('.alert-success').removeClass('show').hide();
                            window.open('/', '_parent');
                           
                        }, 3000);
                    }
                    else {
                        $('.alert-danger').addClass('show').show();
                        $scope.failure = 'Username or password not correct!';
                        setTimeout(() => $('.alert-danger').removeClass('show').hide(), 3000);
                    }
                },
                reject => console.log(reject));
        }
    }
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


