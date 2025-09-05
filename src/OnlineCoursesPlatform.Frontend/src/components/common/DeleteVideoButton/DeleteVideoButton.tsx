import { IconButton } from "@mui/material";
import DeleteTwoToneIcon from '@mui/icons-material/DeleteTwoTone';
import { deleteCloudinaryVideo } from "../../../services/cloudinaryService";
import { toast } from "react-toastify";

interface DeleteVideoButtonProps {
  videoUrl: string | null | undefined;
  onDelete: () => void;
  extractPublicIdFromUrl: (url: string) => string;
  icon?: React.ReactNode;
}

const DeleteVideoButton: React.FC<DeleteVideoButtonProps> = ({
  videoUrl,
  onDelete,
  extractPublicIdFromUrl,
  icon,
}) => {
  const handleDelete = async (e: React.MouseEvent) => {
    e.stopPropagation();

    const confirmDelete = window.confirm(
      "Are you sure you want to delete this video?"
    );
    if (!confirmDelete) return;

    try {
      if (videoUrl) {
        const publicId = extractPublicIdFromUrl(videoUrl);
        await deleteCloudinaryVideo(publicId);
      }
      toast.success("Video deleted successfully.");
      onDelete();
    } catch (error) {
      console.error("Failed to delete video from Cloudinary", error);
      toast.error("Failed to delete video from Cloudinary.");
    }
  };

  return (
    <IconButton onClick={handleDelete} sx={{
            transition: "background-color 0.3s, color 0.3s",
            "&:hover": {
              backgroundColor: "#38424D",
              "& svg": { color: "#FF4C4C" },
            },
          }}>
      {icon || <DeleteTwoToneIcon sx={{ color: "#A5B1C2" }} />}
    </IconButton>
  );
};

export default DeleteVideoButton;
