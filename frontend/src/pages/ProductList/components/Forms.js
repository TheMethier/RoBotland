import { TextField, Button, Box, Select, MenuItem, FormControl, InputLabel  } from '@mui/material';
import React from 'react';
import './Forms.css';
import { useEffect, useState } from 'react';

export const Forms = ({filter, onFilterChange}) => {
    
  const [selectedRow, setSelectedRow] = useState(null);
  const [categories, setCategories] = useState([]);



  const handleInputChange = (name, value) => {
    onFilterChange({ ...filter, [name]: value });
  };

  const handleClearFilters = () => {
    onFilterChange({
      MinPrice: '',
      MaxPrice: '',
      ProductName:'',
    });
  };

  useEffect(() => {
    fetch(`${process.env.REACT_APP_API_URL}/api/v1/products/Product/categories`)
      .then((response) => response.json())
      .then((data) => setCategories(data))
      .catch((error) => console.log(error));
  }, [filter]);

  const handleRowClick = (params) => {
    setSelectedRow(params.id === selectedRow ? null : params.id);
    const categoryId = params.id;
    onFilterChange({ ...filter, categoryId });
  };

  return (
    <Box className="MainBox">
      <Box className="BoxWidth">
      <TextField
          id="TextInput"
          label="Szukaj"
          name="ProductName"
          value={filter.ProductName}
          onChange={(e) => handleInputChange('ProductName', e.target.value)}
      />
      </Box>
      <table id="table">
        <thead>
          <tr>
            <th>Kategorie</th>
          </tr>
        </thead>
        <tbody>
          {categories.map((category) => (
            <tr key={category.id}
                onClick={() => handleRowClick(category)}className={
                  category.id === selectedRow && filter.categoryId === category.id
                    ? 'clicked'
                    : ''
                }
            >
              <td>{category.name}</td>
            </tr>
          ))}
        </tbody>
      </table>
  Cena
  <Box className="BoxWidth">
    <TextField
      id="TextInput"
      label="od"
      name="MinPrice"
      value={filter.MinPrice}
      onChange={(e) => handleInputChange('MinPrice', e.target.value)}

    />
      <TextField
      id="TextInput"
      label="do"
      name="MaxPrice"
      value={filter.MaxPrice} 
      onChange={(e) => handleInputChange('MaxPrice', e.target.value)}
    />
    </Box>
    Dostępność
    <Box className="BoxWidth" mr={2}>
      <FormControl fullWidth >
        <InputLabel >Dostępność</InputLabel>
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
