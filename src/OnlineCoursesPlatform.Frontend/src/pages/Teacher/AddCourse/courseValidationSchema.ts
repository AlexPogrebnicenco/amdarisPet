import * as yup from "yup";

export const courseSchema = yup.object({
  title: yup
    .string()
    .required("Title is required")
    .min(2, "Title must be at least 2 characters")
    .max(100, "Title must be at most 100 characters"), 
  description: yup
    .string()
    .required("Description is required")
    .min(10, "Description must be at least 10 characters")
    .max(4000, "Description must be at most 4000 characters"), 
  difficulty: yup
    .string()
    .oneOf(["Beginner", "Intermediate", "Advanced"])
    .required("Difficulty is required"), 
  about: yup
    .string()
    .notRequired()
    .default("empty"), 
});
