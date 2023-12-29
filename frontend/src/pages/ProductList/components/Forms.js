import { TextField, Button, Box, Select, MenuItem, FormControl, InputLabel  } from '@mui/material';
import React from 'react';
import './Forms.css';

export const Forms = ({filter, onFilterChange}) => {
  
  const handleInputChange = (name, value) => {
    onFilterChange({ ...filter, [name]: value });
  };

  const handleClearFilters = () => {
    onFilterChange({});
  };

  return (
    <Box className="MainBox"   >
      <h3>Filtry</h3>
            Cena
            <Box className="BoxWidth" mr={2}>
              <TextField
                label="od"
                name="MinPrice"
                value={filter.minValue}
                onChange={(e) => handleInputChange('MinPrice', e.target.value)}
      
              />
               <TextField
                label="do"
                name="MaxPrice"
                value={filter.maxValue} 
                onChange={(e) => handleInputChange('MaxPrice', e.target.value)}
              />
            </Box>
            Dostępność
            <Box className="BoxWidth" mr={2}>
              <FormControl fullWidth>
                <InputLabel>Dostępność</InputLabel>
                <Select
                  label="wybierz jedno z trzech 0, 1 lub 2"
                  name="IsAvailable"
                  value={filter.IsAvailable || ''}
                  onChange={(e) => handleInputChange('IsAvailable', e.target.value)}
                >
                  <MenuItem value={0}>0</MenuItem>
                  <MenuItem value={1}>1</MenuItem>
                  <MenuItem value={2}>2</MenuItem>
                </Select>
              </FormControl>
            </Box>
            <Button variant="outlined" onClick={handleClearFilters}> 
              Wyczyść filtry
            </Button>
          </Box>
)};
export default Forms;
