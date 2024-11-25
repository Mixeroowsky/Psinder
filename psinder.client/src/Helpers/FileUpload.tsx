import { Input } from "@/Components/ui/input";
import { Label } from "@/Components/ui/label";
import { useState } from "react";
interface FileUploadProps {
  field: any;
}
const FileUpload = ({ field }: FileUploadProps) => {
  const [image, setImage] = useState<string | null>(null);

  const handleFileChange = (e: any) => {
    const file = e.target.files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        setImage(reader.result as string);
        field.onChange(file);
      };
      reader.readAsDataURL(file);
    }
  };

  return (
    <div className="file-upload rounded-md">
      {image && (
        <div className="preview">
          <img
            src={image}
            alt="Uploaded Preview"
            className="border border-solid  rounded-lg p-2 max-w-lg max-h-96 overflow-hidden"
          />
        </div>
      )}
      <Label htmlFor="picture">Picture</Label>
      <Input
        className="h-16 max-w-sm "
        id="picture"
        accept=".jpg,.jpeg,.png"
        type="file"
        onChange={handleFileChange}
      />
    </div>
  );
};

export default FileUpload;
