import { useForm, useFieldArray } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { lessonSchema } from "./lessonValidationSchema";
import {
  Box,
  Divider,
  IconButton,
  Tooltip,
  Typography,
  useTheme,
} from "@mui/material";
import AddCircleTwoToneIcon from "@mui/icons-material/AddCircleTwoTone";
import CancelTwoToneIcon from "@mui/icons-material/CancelTwoTone";
import CustomTextField from "../../../components/common/CustomTextField/CustomTextField";
import CommonButton from "../../../components/common/CommonButton/CommonButton";
import type { InferType } from "yup";
import { createLesson, type LessonList } from "../../../services/lessonService";
import DeleteVideoButton from "../../../components/common/DeleteVideoButton/DeleteVideoButton";
import UploadVideoInput from "../../../components/common/UploadVideoInput/UploadVideoInput";
import { toast } from "react-toastify";
import { Controller } from "react-hook-form";
import MDEditor from "@uiw/react-md-editor";

type LessonFormInputs = InferType<typeof lessonSchema>;

interface AddLessonFormProps {
  onLessonCreated?: () => void;
  existingLessons: LessonList[];
  courseId: number;
  courseTitle: string;
}

const AddLessonForm: React.FC<AddLessonFormProps> = ({
  onLessonCreated,
  existingLessons,
  courseId,
  courseTitle,
}) => {
  const theme = useTheme();

  const {
    register,
    control,
    handleSubmit,
    formState: { errors },
    reset,
    setValue,
    watch,
  } = useForm<LessonFormInputs>({
    resolver: yupResolver(lessonSchema),
    defaultValues: {
      resourcesArray: [{ url: "" }],
      videoUrls: [{ url: "", title: "" }],
    },
  });

  const { fields, append, remove } = useFieldArray({
    control,
    name: "resourcesArray",
  });

  const {
    fields: videoFields,
    append: appendVideo,
    remove: removeVideo,
  } = useFieldArray({
    control,
    name: "videoUrls",
  });

  const onSubmit = async (data: LessonFormInputs) => {
    if (!courseId) {
      toast.warn("Please select a course.");
      return;
    }

    const isOrderNumberTaken = existingLessons.some(
      (lesson) =>
        lesson.courseId === courseId && lesson.orderNumber === data.orderNumber
    );

    if (isOrderNumberTaken) {
      toast.error(
        "Order number already exists in this course. Please choose another one."
      );
      return;
    }

    try {
      const resourceArray = data.resourcesArray
        ? data.resourcesArray
            .map((r) => (r.url ?? "").trim())
            .filter((url) => url !== "")
        : [];

      const videoArray = data.videoUrls
        ? data.videoUrls
            .filter((v) => typeof v.url === "string" && v.url.trim() !== "")
            .map((v) => ({
              url: v.url!.trim(),
              title: v.title?.trim() || "Untitled video",
            }))
        : [];

      await createLesson(courseId, {
        title: data.title,
        description: data.description,
        orderNumber: data.orderNumber,
        content: data.content,
        resources: resourceArray,
        videoUrls: videoArray,
      });

      reset();
      if (onLessonCreated) onLessonCreated();
      toast.success("Lesson created successfully!");
    } catch (error) {
      console.error("Error creating lesson", error);
      toast.error("Failed to create lesson.");
    }
  };

  const extractPublicIdFromUrl = (url: string) => {
    const parts = url.split("/upload/");
    if (parts.length < 2) return "";

    const pathAfterUpload = parts[1].split(".mp4")[0];
    const segments = pathAfterUpload.split("/");
    const publicId = segments[segments.length - 1];

    return publicId;
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      sx={{
        display: "flex",
        flexDirection: "column",
        gap: 2,
        width: "100%",
        mx: "auto",
      }}
    >
      <Typography
        variant="subtitle1"
        align="center"
        sx={{
          color: theme.palette.text.primary,
          mb: 1,
        }}
      >
        Add New Lesson
      </Typography>

      <Typography
        variant="h5"
        align="center"
        sx={{
          color: theme.palette.text.secondary,
          mb: 2,
        }}
      >
        Course: <strong>{courseTitle}</strong>
      </Typography>

      <CustomTextField
        label="Lesson Title"
        {...register("title")}
        error={!!errors.title}
        helperText={errors.title?.message}
        required
      />

      <CustomTextField
        label="Lesson Description"
        multiline
        minRows={3}
        maxRows={15}
        inputProps={{ maxLength: 4000 }}
        {...register("description")}
        error={!!errors.description}
        helperText={errors.description?.message}
        required
      />

      <CustomTextField
        label="Order Number"
        type="number"
        {...register("orderNumber")}
        error={!!errors.orderNumber}
        helperText={errors.orderNumber?.message}
        required
      />

      <Controller
        name="content"
        control={control}
        render={({ field }) => (
          <Box data-color-mode="dark">
            <Typography variant="h6" sx={{ my: 2, textAlign: "center" }}>
              Content
            </Typography>
            <Box
              sx={{
                p: 1,
                borderRadius: 3,
                background: `linear-gradient(
            135deg,
            #021c2a 0%,
            #033d5b 25%,
            #022b3a 50%,
            #033d5b 75%,
            #010b11 100%
          )`,
                boxShadow: "0 8px 24px rgba(0, 0, 0, 0.5)",
              }}
            >
              <MDEditor
                value={field.value ?? ""}
                onChange={(val) => field.onChange(val || "")}
                height={400}
              />
            </Box>
            {errors.content && (
              <Typography variant="caption" color="error">
                {errors.content.message}
              </Typography>
            )}
          </Box>
        )}
      />

      {/* Resourses */}
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          gap: 2,
        }}
      >
        <Typography variant="h6">Resources</Typography>
        <IconButton
          onClick={(e) => {
            e.stopPropagation();
            append({ url: "" });
          }}
          sx={{
            transition: "background-color 0.3s, color 0.3s",
            "&:hover": {
              backgroundColor: "#38424D",
              "& svg": { color: "#00B300" },
            },
          }}
        >
          <AddCircleTwoToneIcon sx={{ color: theme.palette.text.primary }} />
        </IconButton>
      </Box>

      {fields.map((field, index) => (
        <Box
          key={field.id}
          sx={{ display: "flex", gap: 1, alignItems: "center" }}
        >
          <CustomTextField
            label={`${index + 1}. Resource URL(optional)`}
            {...register(`resourcesArray.${index}.url` as const)}
            fullWidth
          />
          <IconButton
            onClick={(e) => {
              e.stopPropagation();
              remove(index);
            }}
            sx={{
              transition: "background-color 0.3s, color 0.3s",
              "&:hover": {
                backgroundColor: "#38424D",
                "& svg": { color: "#FF4C4C" },
              },
            }}
          >
            <CancelTwoToneIcon sx={{ color: theme.palette.text.primary }} />
          </IconButton>
        </Box>
      ))}

      {/* Videos */}
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          gap: 2,
        }}
      >
        <Typography variant="h6">Video URLs</Typography>
        <IconButton
          onClick={(e) => {
            e.stopPropagation();
            appendVideo({ url: "", title: "" });
          }}
          sx={{
            transition: "background-color 0.3s, color 0.3s",
            "&:hover": {
              backgroundColor: "#38424D",
              "& svg": { color: "#00B300" },
            },
          }}
        >
          <AddCircleTwoToneIcon sx={{ color: theme.palette.text.primary }} />
        </IconButton>
      </Box>

      {videoFields.map((field, index) => {
        const currentVideoUrls = watch(`videoUrls`);
        const currentVideoUrl = currentVideoUrls?.[index]?.url;

        return (
          <Box key={field.id}>
            <Divider />
            <Box
              key={field.id}
              sx={{ display: "flex", gap: 1, alignItems: "center" }}
            >
              <UploadVideoInput
                name={`videoUrls.${index}`}
                setValue={setValue}
                watch={watch}
                onRemove={() => removeVideo(index)}
              />

              {currentVideoUrl && (
                <Tooltip title="Delete video from storage">
                  <span>
                    <DeleteVideoButton
                      videoUrl={currentVideoUrl}
                      onDelete={() => removeVideo(index)}
                      extractPublicIdFromUrl={extractPublicIdFromUrl}
                    />
                  </span>
                </Tooltip>
              )}
            </Box>
            <Divider />
          </Box>
        );
      })}

      <Box
        sx={{ display: "flex", alignItems: "center", justifyContent: "center" }}
      >
        <CommonButton
          type="submit"
          fullWidth
          sx={{ maxWidth: "400px", mx: "auto", display: "block" }}
        >
          Create Lesson
        </CommonButton>
      </Box>
    </Box>
  );
};

export default AddLessonForm;

export type { LessonFormInputs };
