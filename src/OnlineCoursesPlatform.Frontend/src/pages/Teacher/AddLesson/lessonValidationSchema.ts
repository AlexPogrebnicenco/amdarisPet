import * as yup from "yup";

export const lessonSchema = yup.object({
  title: yup
    .string()
    .required("Title is required")
    .min(2, "Title must be at least 2 characters")
    .max(200, "Title must be at most 200 characters"),

  description: yup
    .string()
    .required("Description is required")
    .min(10, "Description must be at least 10 characters")
    .max(4000, "Description must be at most 4000 characters"),

  orderNumber: yup
    .number()
    .typeError("Order number must be a number")
    .required("Order number is required")
    .min(1, "Order number must be at least 1"),

  content: yup
    .string()
    .notRequired()
    .default("empty"), 

  resourcesArray: yup
    .array()
    .of(
      yup.object().shape({
        url: yup
          .string()
          .nullable()
          .notRequired()
          .test("is-valid-url", "Must be a valid URL", (value) => {
            if (!value || value.trim() === "") {
              return true;
            }
            return yup.string().url().isValidSync(value);
          }),
      })
    )
    .notRequired()
    .default([]),


  videoUrls: yup
  .array()
  .of(
    yup.object().shape({
      url: yup
        .string()
        .nullable()
        .notRequired()
        .test("is-valid-url", "Must be a valid URL", (value) => {
          if (!value || value.trim() === "") {
            return true;
          }
          return yup.string().url().isValidSync(value);
        }),
        title: yup 
          .string()
          .nullable()
          .notRequired(),
    })
  )
  .notRequired()
  .default([]),
});
