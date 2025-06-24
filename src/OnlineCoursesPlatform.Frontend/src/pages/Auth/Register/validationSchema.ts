import * as yup from "yup";

export const registerSchema = yup.object({
    userName: yup
        .string()
        .required("Username is required")
        .min(2, "User name must be between 2 and 100 characters")
        .max(100, "User name must be between 2 and 100 characters"),

    email: yup
        .string()
        .required("Email is required")
        .email("Invalid email"),
    password: yup
        .string()
        .required("Password is required")
        .min(8, "Password must be at least 8 characters long")
        .matches(/[A-Z]/, "Password must contain at least one uppercase letter")
        .matches(/[a-z]/, "Password must contain at least one lowercase letter")
        .matches(/\d/, "Password must contain at least one digit")
        .matches(/[\W_]/, "Password must contain at least one special character"),

    confirmPassword: yup
        .string()
        .required("Confirm your password")
        .oneOf([yup.ref("password")], "Password do not match"),
        
    age: yup
        .number()
        .transform((value, originalValue) =>
            String(originalValue).trim() === "" ? undefined : value
        )
        .typeError("Age must be a number")
        .required("Age is required")
        .min(13, "You must be at least 13 years old"),
            
    gender: yup
        .string()
        .required("Gender is required"),

    role: yup
        .string()
        .oneOf(["User", "Teacher"])
        .required("Role is required"),
});