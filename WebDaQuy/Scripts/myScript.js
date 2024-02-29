$(document).ready(function(){
    // Modal
    $(".js-login").click(function(){
        $(".modal").addClass("open");
        $(".login").show();
    });
    $(".modal").click(function(){
        $(".login").hide();
        $(".modal").removeClass("open");
    });
    $(".close").click(function(){
        $(".login").hide();
        $(".modal").removeClass("open");
    });
    $(".login").click(function(event){
        event.stopPropagation();
    });     

    $(".js-register").click(function(){
        $(".modal").addClass("open");
        $(".register").show();
    });
    $(".modal").click(function(){
        $(".register").hide();
        $(".modal").removeClass("open");
    });
    $(".close").click(function(){
        $(".register").hide();
        $(".modal").removeClass("open");
    });
    $(".register").click(function(event){
        event.stopPropagation();
    });

    $(".js-giayViet").click(function(){
        $(".modal").addClass("open");
        $(".giayViet").show();
    });
    $(".modal").click(function(){
        $(".giayViet").hide();
        $(".modal").removeClass("open");
    });
    $(".close").click(function(){
        $(".giayViet").hide();
        $(".modal").removeClass("open");
    });
    $(".giayViet").click(function(event){
        event.stopPropagation();
    });

    $(".js-mucViet").click(function(){
        $(".modal").addClass("open");
        $(".mucViet").show();
    });
    $(".modal").click(function(){
        $(".mucViet").hide();
        $(".modal").removeClass("open");
    });
    $(".close").click(function(){
        $(".mucViet").hide();
        $(".modal").removeClass("open");
    });
    $(".mucViet").click(function(event){
        event.stopPropagation();
    });
    //Slick
    $('.home-collection-body').slick({
        infinite: false,
        slidesToShow: 5,
        slidesToScroll: 1,
        arrows: true,
        prevArrow:"<button type='button' class='slick-prev pull-left'><i class='fa fa-angle-left' aria-hidden='true'></i></button>",
        nextArrow:"<button type='button' class='slick-next pull-right'><i class='fa fa-angle-right' aria-hidden='true'></i></button>"
    });
    $('.zoom-Small').slick({
        infinite: false,
        slidesToShow: 4.5,
        slidesToScroll: 3,
        arrows: false
    });
    // Zoom image
    $('.zoom-imgSmall').click(function(){
        $('.zoom-imgBig').attr('src',$(this).attr('src'));
        $('.zoom-imgSmall').ready(function(){
            $('.zoom-imgBig').add('data-zoom-image');
            $('.zoom-imgBig').attr('data-zoom-image',$(this).attr('src'));
            $('.zoom-imgBig').elevateZoom({
                zoomWindowWidth: 310,
                zoomWindowHeight: 340,
            });
        }); 
        $('.zoom-imgBig').remove('data-zoom-image');
    }); 
    $('.product-details > .btn').click(function(){
        $('.product-details > .btn').removeClass('tick');
        $(this).addClass('tick');
    });
    $('.mauCoDam').click(function(){
        $('.zoom-imgBig').attr('src',$('.zoom-imgSmall.mauCoDam').attr('src'));
        $('.zoom-imgSmall.mauCoDam').ready(function(){
            $('.zoom-imgBig').add('data-zoom-image');
            $('.zoom-imgBig').attr('data-zoom-image',$(this).attr('src'));
            $('.zoom-imgBig').elevateZoom({
                zoomWindowWidth: 310,
                zoomWindowHeight: 340,
            });
        }); 
        $('.zoom-imgBig').remove('data-zoom-image');
    }); 
    $('.mauCoNhat').click(function(){
        $('.zoom-imgBig').attr('src',$('.zoom-imgSmall.mauCoNhat').attr('src'));
        $('.zoom-imgSmall.mauCoNhat').ready(function(){
            $('.zoom-imgBig').add('data-zoom-image');
            $('.zoom-imgBig').attr('data-zoom-image',$(this).attr('src'));
            $('.zoom-imgBig').elevateZoom({
                zoomWindowWidth: 310,
                zoomWindowHeight: 340,
            });
        }); 
        $('.zoom-imgBig').remove('data-zoom-image');
    }); 
    $('.mauXanhGhi').click(function(){
        $('.zoom-imgBig').attr('src',$('.zoom-imgSmall.mauXanhGhi').attr('src'));
        $('.zoom-imgSmall.mauXanhGhi').ready(function(){
            $('.zoom-imgBig').add('data-zoom-image');
            $('.zoom-imgBig').attr('data-zoom-image',$(this).attr('src'));
            $('.zoom-imgBig').elevateZoom({
                zoomWindowWidth: 310,
                zoomWindowHeight: 340,
            });
        }); 
        $('.zoom-imgBig').remove('data-zoom-image');
    }); 
    
    //back-to-top
    const toTop = document.querySelector(".back-to-top");
    window.addEventListener("scroll", () => {
        if (window.pageYOffset > 200) {
            toTop.classList.add("active");
        } else{
            toTop.classList.remove("active");
        }
    })  
});
