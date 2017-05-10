using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Media.MediaProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using ZXing;
using ZXing.Common;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SoLib.Controls
{
    public sealed partial class QRScanner : UserControl
    {
        private readonly MediaCapture _mediaCapture;
        private Result _result;
        private bool _cameraInitialized;

        public event EventHandler<QRCodeFoundEventArgs> QRCodeFound;

        public QRScanner()
        {
            this.InitializeComponent();
            _mediaCapture = new MediaCapture();
            this.Loaded += QRScanner_Loaded;
        }

        private async Task InitializeCameraAsync()
        {
            MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings()
            {
                StreamingCaptureMode = StreamingCaptureMode.Video,
                PhotoCaptureSource = PhotoCaptureSource.VideoPreview,
                AudioDeviceId = string.Empty
            };

            // 检测摄像头，优先后置摄像头
            var allVedioDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
            var desiredDevice = allVedioDevices.FirstOrDefault(d => d.EnclosureLocation != null && d.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Back);
            if (desiredDevice != null)
            {
                settings.VideoDeviceId = desiredDevice.Id;
            }
            await _mediaCapture.InitializeAsync(settings);
            qrCode.Source = _mediaCapture;
            _mediaCapture.SetPreviewRotation(VideoRotation.Clockwise90Degrees);

            // 设置2倍焦距
            //_mediaCapture.VideoDeviceController.ZoomControl.Value = 2f;
            if (_mediaCapture.VideoDeviceController.FocusControl.Supported && _mediaCapture.VideoDeviceController.FocusControl.WaitForFocusSupported)
            {
                // 设置自动对焦
                _mediaCapture.VideoDeviceController.FocusControl.Configure(new FocusSettings()
                {
                    Mode = FocusMode.Continuous
                });
            }

            await _mediaCapture.StartPreviewAsync();
            if (_mediaCapture.VideoDeviceController.FocusControl.Supported && _mediaCapture.VideoDeviceController.FocusControl.WaitForFocusSupported)
            {
                // 对焦
                await _mediaCapture.VideoDeviceController.FocusControl.FocusAsync();
            }

            _cameraInitialized = true;
        }

        private async Task ScanAsync()
        {
            if (_cameraInitialized)
            {
                _result = null;
                while (_result == null)
                {
                    // 对焦
                    await _mediaCapture.VideoDeviceController.FocusControl.FocusAsync();
                    var previewProperties = _mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview) as VideoEncodingProperties;
                    if (previewProperties != null)
                    {
                        var videoFrame = new VideoFrame(BitmapPixelFormat.Bgra8, (int)previewProperties.Width, (int)previewProperties.Height);
                        using (var currentFrame = await _mediaCapture.GetPreviewFrameAsync(videoFrame))
                        {
                            var frameBitmap = currentFrame.SoftwareBitmap;
                            if (frameBitmap != null)
                            {
                                var bitmap = new WriteableBitmap(frameBitmap.PixelWidth, frameBitmap.PixelHeight);
                                frameBitmap.CopyToBuffer(bitmap.PixelBuffer);

                                // 解析二维码
                                BarcodeReader barcodeReader = new BarcodeReader()
                                {
                                    AutoRotate = true,
                                    Options = new DecodingOptions() { TryHarder = true }
                                };
                                _result = barcodeReader.Decode(bitmap);
                                if (_result != null)
                                {
                                    Debug.WriteLine($"扫描到二维码:{_result.Text}");
                                    QRCodeFound?.Invoke(qrCode, new QRCodeFoundEventArgs() { Result = _result });
                                }
                            }
                        }
                    }
                }
            }
        }

        private async void QRScanner_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeCameraAsync();
            await ScanAsync();
        }
    }

    public class QRCodeFoundEventArgs : EventArgs
    {
        public Result Result { get; set; }
    }
}