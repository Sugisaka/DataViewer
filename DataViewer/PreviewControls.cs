using System.Windows.Controls;

namespace DataViewer
{
    /// <summary>
    /// プレビュー表示の更新に使用するUI要素。
    /// </summary>
    public sealed class PreviewControls
    {
        public required Slider MagnificationSlider { get; init; }
        public required TextBox MagnificationTextBox { get; init; }
        public required Grid PreviewGrid { get; init; }
        public required Image PreviewImage { get; init; }
        public required TextBox NxTextBox { get; init; }
        public required TextBox NyTextBox { get; init; }
        public required TextBox MinTextBox { get; init; }
        public required TextBox MaxTextBox { get; init; }
        public required Label MinLabel { get; init; }
        public required Label MaxLabel { get; init; }
    }
}
