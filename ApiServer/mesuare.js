//const Cesium = window.Cesium;
const DWG = (function() {
  function _() {}
  _.init = function(container, data) {
    //使用cesium默认资源 需要的token;
    var defaultAccessToken =
      "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJhNDI4OTI1Ni02YWY0LTQ0OTgtYTI0My1hNDYyMDRhNmM0ZTAiLCJpZCI6MjQxMjEsInNjb3BlcyI6WyJhc3IiLCJnYyJdLCJpYXQiOjE1ODQ2MDY3ODh9.kq8I2ze8aHAMl1bf9_y0GBxfeu9_OWnYOxkgGohIjvc";
    if (data.defaultAccessToken && data.defaultAccessToken != "") {
      defaultAccessToken = data.defaultAccessToken;
    }
    Cesium.Ion.defaultAccessToken = defaultAccessToken;

    //初始化部分参数 如果没有就是false;
    var args = [
      "geocoder",
      "homeButton",
      "sceneModePicker",
      "baseLayerPicker",
      "navigationHelpButton",
      "animation",
      "timeLine",
      "fullscreenButton",
      "vrButton",
      "infoBox",
      "selectionIndicator"
    ];
    for (var i = 0; i < args.length; i++) {
      if (!data[args[i]]) {
        data[args[i]] = false;
      }
    }

    //创建viewer
    var viewer = new Cesium.Viewer(container, data); //cesium初始化的时候 data中的参数不存在 也没事。

    var img, label;
    //取消双击选中事件。(这个作用不大)
    viewer.cesiumWidget.screenSpaceEventHandler.removeInputAction(
      Cesium.ScreenSpaceEventType.LEFT_DOUBLE_CLICK
    );
    //是否添加全球光照，scene(场景)中的光照将会随着每天时间的变化而变化
    if (data.globeLight && data.globeLight == true) {
      viewer.scene.globe.enableLighting = true;
    }
    //是否关闭大气效果
    if (data.showGroundAtmosphere && data.showGroundAtmosphere == true) {
      viewer.scene.globe.showGroundAtmosphere = true;
    } else {
      viewer.scene.globe.showGroundAtmosphere = false;
    }
    //地图开发者密钥
    if (!data.defaultKey || data.defaultKey == "") {
      data.defaultKey = "19b72f6cde5c8b49cf21ea2bb4c5b21e";
    }
    //天地图影像
    if (data.globalImagery && data.globalImagery == "天地图") {
      viewer.imageryLayers.remove(viewer.imageryLayers.get(0)); //可以先清除默认的第一个影像 bing地图影像。 当然不作处理也行
      var url =
        "http://t0.tianditu.com/img_w/wmts?service=wmts&request=GetTile&version=1.0.0&LAYER=img&tileMatrixSet=w&TileMatrix={TileMatrix}&TileRow={TileRow}&TileCol={TileCol}&style=default&format=tiles" +
        "&tk=" +
        data.defaultKey;
      img = viewer.imageryLayers.addImageryProvider(
        new Cesium.WebMapTileServiceImageryProvider({
          url: url,
          layer: "tdtBasicLayer",
          style: "default",
          format: "image/jpeg",
          maximumLevel: 18, //天地图的最大缩放级别
          tileMatrixSetID: "GoogleMapsCompatible",
          show: false
        })
      );
    }
    //谷歌影像
    else if (data.globalImagery && data.globalImagery == "谷歌") {
      viewer.imageryLayers.remove(viewer.imageryLayers.get(0)); //可以先清除默认的第一个影像 bing地图影像。 当然不作处理也行
      img = viewer.imageryLayers.addImageryProvider(
        new Cesium.UrlTemplateImageryProvider({
          url:
            "http://mt1.google.cn/vt/lyrs=s&hl=zh-CN&x={x}&y={y}&z={z}&s=Gali",
          baseLayerPicker: false
        })
      );
    }
    //arcGis影像
    else if (data.globalImagery && data.globalImagery == "arcGis") {
      viewer.imageryLayers.remove(viewer.imageryLayers.get(0)); //可以先清除默认的第一个影像 bing地图影像。 当然不作处理也行
      img = viewer.imageryLayers.addImageryProvider(
        new Cesium.ArcGisMapServerImageryProvider({
          url:
            "https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer",
          baseLayerPicker: false
        })
      );
    }
    //高德影像
    else if (data.globalImagery && data.globalImagery == "高德") {
      viewer.imageryLayers.remove(viewer.imageryLayers.get(0)); //可以先清除默认的第一个影像 bing地图影像。 当然不作处理也行
      img = viewer.imageryLayers.addImageryProvider(
        new Cesium.UrlTemplateImageryProvider({
          maximumLevel: 18, //最大缩放级别
          url:
            "https://webst02.is.autonavi.com/appmaptile?style=6&x={x}&y={y}&z={z}",
          style: "default",
          format: "image/png",
          tileMatrixSetID: "GoogleMapsCompatible"
        })
      );
    }
    //百度影像
    else if (data.globalImagery && data.globalImagery == "百度") {
      viewer.imageryLayers.remove(viewer.imageryLayers.get(0)); //可以先清除默认的第一个影像 bing地图影像。 当然不作处理也行
      img = viewer.imageryLayers.addImageryProvider(
        new Cesium.UrlTemplateImageryProvider({
          maximumLevel: 18, //最大缩放级别
          url:
            "https://ss1.bdstatic.com/8bo_dTSlR1gBo1vgoIiO_jowehsv/tile/?qt=vtile&x={x}&y={y}&z={z}&styles=pl&udt=20180810&scaler=1&showtext=1"
        })
      );
    }

    //天地图标注
    if (data.globalLabel && data.globalLabel == "天地图") {
      const url =
        "http://t0.tianditu.com/cia_w/wmts?service=wmts&request=GetTile&version=1.0.0&LAYER=cia&tileMatrixSet=w&TileMatrix={TileMatrix}&TileRow={TileRow}&TileCol={TileCol}&style=default.jpg" +
        "&tk=" +
        data.defaultKey;
      label = viewer.imageryLayers.addImageryProvider(
        new Cesium.WebMapTileServiceImageryProvider({
          url: url,
          layer: "tdtAnnoLayer",
          style: "default",
          maximumLevel: 18, //天地图的最大缩放级别
          format: "image/jpeg",
          tileMatrixSetID: "GoogleMapsCompatible",
          show: false
        })
      );
    }
    //高德标注
    else if (data.globalLabel && data.globalLabel == "高德") {
      label = viewer.imageryLayers.addImageryProvider(
        new Cesium.UrlTemplateImageryProvider({
          maximumLevel: 18, //最大缩放级别
          url:
            "https://wprd02.is.autonavi.com/appmaptile?x={x}&y={y}&z={z}&lang=zh_cn&size=1&scl=2&style=8&ltype=11",
          style: "default",
          format: "image/png",
          tileMatrixSetID: "GoogleMapsCompatible"
        })
      );
    }
    //影像亮度
    if (data.globalImageryBrightness != undefined) {
      img.brightness = data.globalImageryBrightness;
    }
    if (data.globalLabelBrightness != undefined) {
      label.brightness = data.globalLabelBrightness;
    }

    return viewer;
  };
  _.measureArea = function(viewer) {
    // 取消双击事件-追踪该位置
    viewer.cesiumWidget.screenSpaceEventHandler.removeInputAction(
      Cesium.ScreenSpaceEventType.LEFT_DOUBLE_CLICK
    );
    // 鼠标事件
    const handler = new Cesium.ScreenSpaceEventHandler(
      viewer.scene._imageryLayerCollection
    );
    var positions = [];
    var tempPoints = [];
    var polygon = null;

    var cartesian = null;
    var floatingPoint; //浮动点
    var callFun;
    handler.setInputAction(function(movement) {
      // cartesian = viewer.scene.pickPosition(movement.endPosition);
      let ray = viewer.camera.getPickRay(movement.endPosition);
      cartesian = viewer.scene.globe.pick(ray, viewer.scene);
      // cartesian = viewer.scene.camera.pickEllipsoid(movement.endPosition, viewer.scene.globe.ellipsoid);
      if (positions.length >= 2) {
        if (!Cesium.defined(polygon)) {
          polygon = new PolygonPrimitive(positions);
          // callFun = new Cesium.CallbackProperty(() => {
          //   if (polygon) {
          //     //viewer.entities.remove(polygon);
          //   }
          //   polygon = viewer.entities.add({
          //     name: "Red polygon on surface",
          //     polygon: {
          //       hierarchy: positions,
          //       material: Cesium.Color.GREEN.withAlpha(0.5)
          //     }
          //   });
          //   return positions;
          // }, false);
          // polygon = viewer.entities.add({
          //   name: "Red polygon on surface",
          //   polygon: {
          //     hierarchy: new Cesium.CallbackProperty(() => {
          //       return positions;
          //     }, false), //positions,
          //     material: Cesium.Color.GREEN.withAlpha(0.5)
          //   }
          // });
        } else {
          positions.pop();
          //polygon.polygon.hierarchy = positions;
          // cartesian.y += (1 + Math.random());
          positions.push(cartesian);
          //polygon.polygon.hierarchy = positions;
        }
      }
    }, Cesium.ScreenSpaceEventType.MOUSE_MOVE);

    handler.setInputAction(function(movement) {
      // cartesian = viewer.scene.pickPosition(movement.position);
      let ray = viewer.camera.getPickRay(movement.position);
      cartesian = viewer.scene.globe.pick(ray, viewer.scene);
      // cartesian = viewer.scene.camera.pickEllipsoid(movement.position, viewer.scene.globe.ellipsoid);
      if (positions.length == 0) {
        positions.push(cartesian.clone());
      }
      //positions.pop();
      positions.push(cartesian);
      if (Cesium.defined(polygon)) {
        //polygon.polygon.hierarchy = positions;
      }
      //在三维场景中添加点
      var cartographic = Cesium.Cartographic.fromCartesian(
        positions[positions.length - 1]
      );
      var longitudeString = Cesium.Math.toDegrees(cartographic.longitude);
      var latitudeString = Cesium.Math.toDegrees(cartographic.latitude);
      var heightString = cartographic.height;
      tempPoints.push({
        lon: longitudeString,
        lat: latitudeString,
        hei: heightString
      });
      floatingPoint = viewer.entities.add({
        name: "多边形面积",
        position: positions[positions.length - 1],
        point: {
          pixelSize: 5,
          color: Cesium.Color.RED,
          outlineColor: Cesium.Color.WHITE,
          outlineWidth: 2,
          heightReference: Cesium.HeightReference.CLAMP_TO_GROUND
        }
      });
    }, Cesium.ScreenSpaceEventType.LEFT_CLICK);

    handler.setInputAction(function(movement) {
      handler.destroy();
      positions.pop();
      if (Cesium.defined(polygon)) {
        //polygon.polygon.hierarchy = positions;
      }
      //在三维场景中添加点
      // var cartographic = Cesium.Cartographic.fromCartesian(positions[positions.length - 1]);
      // var longitudeString = Cesium.Math.toDegrees(cartographic.longitude);
      // var latitudeString = Cesium.Math.toDegrees(cartographic.latitude);
      // var heightString = cartographic.height;
      // tempPoints.push({ lon: longitudeString, lat: latitudeString ,hei:heightString});

      var textArea = getArea(tempPoints) + "平方公里";
      viewer.entities.add({
        name: "多边形面积",
        position: positions[positions.length - 1],
        // point : {
        //  pixelSize : 5,
        //  color : Cesium.Color.RED,
        //  outlineColor : Cesium.Color.WHITE,
        //  outlineWidth : 2,
        //  heightReference:Cesium.HeightReference.CLAMP_TO_GROUND
        // },
        label: {
          text: textArea,
          font: "18px sans-serif",
          fillColor: Cesium.Color.GOLD,
          style: Cesium.LabelStyle.FILL_AND_OUTLINE,
          outlineWidth: 2,
          verticalOrigin: Cesium.VerticalOrigin.BOTTOM,
          pixelOffset: new Cesium.Cartesian2(20, -40),
          heightReference: Cesium.HeightReference.CLAMP_TO_GROUND
        }
      });
    }, Cesium.ScreenSpaceEventType.RIGHT_CLICK);

    var radiansPerDegree = Math.PI / 180.0; //角度转化为弧度(rad)
    var degreesPerRadian = 180.0 / Math.PI; //弧度转化为角度

    //计算多边形面积
    function getArea(points) {
      var res = 0;
      //拆分三角曲面

      for (var i = 0; i < points.length - 2; i++) {
        var j = (i + 1) % points.length;
        var k = (i + 2) % points.length;
        var totalAngle = Angle(points[i], points[j], points[k]);

        var dis_temp1 = distance(positions[i], positions[j]);
        var dis_temp2 = distance(positions[j], positions[k]);
        res += dis_temp1 * dis_temp2 * Math.abs(Math.sin(totalAngle));
        console.log(res);
      }

      return (res / 1000000.0).toFixed(4);
    }

    /*角度*/
    function Angle(p1, p2, p3) {
      var bearing21 = Bearing(p2, p1);
      var bearing23 = Bearing(p2, p3);
      var angle = bearing21 - bearing23;
      if (angle < 0) {
        angle += 360;
      }
      return angle;
    }
    /*方向*/
    function Bearing(from, to) {
      var lat1 = from.lat * radiansPerDegree;
      var lon1 = from.lon * radiansPerDegree;
      var lat2 = to.lat * radiansPerDegree;
      var lon2 = to.lon * radiansPerDegree;
      var angle = -Math.atan2(
        Math.sin(lon1 - lon2) * Math.cos(lat2),
        Math.cos(lat1) * Math.sin(lat2) -
          Math.sin(lat1) * Math.cos(lat2) * Math.cos(lon1 - lon2)
      );
      if (angle < 0) {
        angle += Math.PI * 2.0;
      }
      angle = angle * degreesPerRadian; //角度
      return angle;
    }

    var PolygonPrimitive = (function() {
      function _(positions) {
        this.options = {
          name: "多边形",
          polygon: {
            hierarchy: [],
            // perPositionHeight : true,
            material: Cesium.Color.GREEN.withAlpha(0.5)
            // heightReference:20000
          }
        };

        this.hierarchy = positions;
        this._init();
      }

      _.prototype._init = function() {
        var _self = this;
        var _update = function() {
          return _self.hierarchy;
        };
        //实时更新polygon.hierarchy
        this.options.polygon.hierarchy = new Cesium.CallbackProperty(
          _update,
          false
        );
        viewer.entities.add(this.options);
      };

      return _;
    })();

    function distance(point1, point2) {
      var point1cartographic = Cesium.Cartographic.fromCartesian(point1);
      var point2cartographic = Cesium.Cartographic.fromCartesian(point2);
      /**根据经纬度计算出距离**/
      var geodesic = new Cesium.EllipsoidGeodesic();
      geodesic.setEndPoints(point1cartographic, point2cartographic);
      var s = geodesic.surfaceDistance;
      //console.log(Math.sqrt(Math.pow(distance, 2) + Math.pow(endheight, 2)));
      //返回两点之间的距离
      s = Math.sqrt(
        Math.pow(s, 2) +
          Math.pow(point2cartographic.height - point1cartographic.height, 2)
      );
      return s;
    }
  };
  _.addLayer = function(viewer, url) {
    var layers = viewer.scene.imageryLayers;
    //url = `http://192.168.1.80:6080/arcgis/rest/services/xzq_hmx/MapServer`;
    const pLayer = new Cesium.ArcGisMapServerImageryProvider({
      url: url,
      errorEvent: err => {
        console.log(err);
      }
    });
    const layer = layers.addImageryProvider(pLayer);
    return layer;
  };
  _.removeLayer = function(viewer, layer, isAll = false) {
    var layerCollection = viewer.scene.imageryLayers;
    isAll ? layerCollection.removeAll() : layerCollection.remove(layer);
  };
  _.showLayer = function(imagerLayer, isShow) {
    if (imagerLayer instanceof Cesium.ImageryLayer) {
      imagerLayer.show = isShow;
    }
  };
  _.alphaLayer = function(imagerLayer, alpha) {
    if (imagerLayer instanceof Cesium.ImageryLayer && alpha instanceof Number) {
      imagerLayer.alpha = alpha;
    }
  };
  _.brightnessLayer = function(imagerLayer, brightness) {
    if (
      imagerLayer instanceof Cesium.ImageryLayer &&
      brightness instanceof Number
    ) {
      imagerLayer.brightness = brightness;
    }
  };
  return _;
})();
//export default DWG;
