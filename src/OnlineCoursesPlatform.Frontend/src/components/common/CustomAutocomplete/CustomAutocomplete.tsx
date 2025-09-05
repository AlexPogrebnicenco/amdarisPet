import { Autocomplete, type AutocompleteProps } from '@mui/material';
import { useTheme } from '@mui/material/styles';
import CustomTextField from '../CustomTextField/CustomTextField';

interface CustomAutocompleteProps<T, Multiple extends boolean = false>
  extends Omit<AutocompleteProps<T, Multiple, false, false>, 'renderInput' | 'options' | 'getOptionLabel' | 'value' | 'onChange' | 'multiple'> {
  options: T[];
  getOptionLabel: (option: T) => string;
  value: Multiple extends true ? T[] : T | null;
  onChange: (value: Multiple extends true ? T[] : T | null) => void;
  label: string;
  placeholder: string;
  multiple?: Multiple;
  disabled?: boolean;
  textFieldProps?: React.ComponentProps<typeof CustomTextField>; 
  listboxSx?: object;
}

const CustomAutocomplete = <T, Multiple extends boolean = false>({
  options,
  getOptionLabel,
  value,
  onChange,
  label,
  placeholder,
  multiple = false as Multiple,
  disabled = false,
  textFieldProps,
  listboxSx,
  ...autocompleteProps // 👉 сюда попадают любые дополнительные пропсы
}: CustomAutocompleteProps<T, Multiple>) => {
  const theme = useTheme();

  return (
    <Autocomplete
      multiple={multiple}
      options={options}
      getOptionLabel={getOptionLabel}
      value={value}
      onChange={(_, newValue) => onChange(newValue)}
      disabled={disabled}
      renderInput={(params) => (
        <CustomTextField
          {...params}
          label={label}
          placeholder={placeholder}
          {...textFieldProps} // 👉 пробрасываем дополнительные пропсы для TextField
        />
      )}
      slotProps={{
        paper: {
          sx: {
            backgroundColor: "#1C1F23",
            color: theme.palette.text.primary,
            mb: 2,
          },
        },
        listbox: {
          onWheel: (e) => e.stopPropagation(),
          sx: {
            px: 1,
            maxHeight: "250px",
            ...listboxSx, 
            "& .MuiAutocomplete-option": {
              borderRadius: 1,
              "&.Mui-focused": {
                backgroundColor: "#2D3138",
                color: theme.palette.text.secondary,
              },
              '&[aria-selected="true"]': {
                backgroundColor: "#2D3138",
                color: theme.palette.text.secondary,
              },
            },
          },
        },
      }}
      {...autocompleteProps} // 👉 пробрасываем все остальные пропсы для Autocomplete
    />
  );
};

export default CustomAutocomplete;



{/* Одиночный выбор: 
     <CustomAutocomplete<CategoryDto>
  options={categories}
  getOptionLabel={(option) => option.categoryName}
  value={selectedCategory}
  onChange={(newValue) => {
    setSelectedCategory(newValue);
    setSelectedTags([]);
  }}
  label="Select Category"
  placeholder="Category"
/>
 */}

{/* Мультивыбор:
    <CustomAutocomplete
  o<CustomAutocomplete<TagDto, true>
  options={filteredTags}
  getOptionLabel={(option) => option.name}
  value={selectedTags}
  onChange={(newValue) => setSelectedTags(newValue)}
  label="Select Tags"
  placeholder="Tags"
  multiple
  disabled={!selectedCategory}
/> */}