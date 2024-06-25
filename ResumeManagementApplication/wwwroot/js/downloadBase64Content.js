function downloadBase64Content(base64Data, fileName) {
    const link = document.createElement('a');
    link.href = "data:text/plain;base64," + base64Data; // Adjust content type as needed
    link.download = fileName;
    link.click();
}