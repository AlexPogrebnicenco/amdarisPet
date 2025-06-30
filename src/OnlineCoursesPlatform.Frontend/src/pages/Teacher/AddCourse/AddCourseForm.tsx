import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { courseSchema } from "./courseValidationSchema";
import { Box, Typography, useTheme } from "@mui/material";
import CustomTextField from "../../../components/common/CustomTextField/CustomTextField";
import CommonButton from "../../../components/common/CommonButton/CommonButton";
import type { InferType } from "yup";
// import { createCourse } from "../../services/courseService";

type CourseFormInputs = InferType<typeof courseSchema>;

interface AddCourseFormProps {
  onCourseCreated?: () => void;
}

const AddCourseForm: React.FC<AddCourseFormProps> = ({ onCourseCreated }) => {
  const theme = useTheme();

  const { register, handleSubmit, formState: { errors }, reset } = useForm<CourseFormInputs>({
    resolver: yupResolver(courseSchema),
  });

//   const onSubmit = async (data: CourseFormInputs) => {
//     try {
//       await createCourse(data);
//       reset();
//       if (onCourseCreated) onCourseCreated();
//       alert("Course created successfully!");
//     } catch (error) {
//       console.error("Error creating course", error);
//       alert("Failed to create course.");
//     }
//   };

  return (
    <Box
      component="form"
    //   onSubmit={handleSubmit(onSubmit)}
      sx={{
        display: "flex",
        flexDirection: "column",
        gap: 2,
        width: "100%",
        maxWidth: 600,
        mx: "auto",
        p: 2,
      }}
    >
      <Typography
        variant="h5"
        align="center"
        sx={{
          color: theme.palette.text.secondary,
          mb: 2,
        }}
      >
        Add New Course
      </Typography>

      <CustomTextField
        label="Course Title"
        {...register("title")}
        error={!!errors.title}
        helperText={errors.title?.message}
      />

      <CustomTextField
        label="Course Description"
        multiline
        rows={4}
        {...register("description")}
        error={!!errors.description}
        helperText={errors.description?.message}
      />

      <CommonButton type="submit" fullWidth>
        Create Course
      </CommonButton>
    </Box>
  );
};

export default AddCourseForm;
