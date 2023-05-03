import { json } from "react-router-dom";

class CategoryService {
    async getCategories() {
        const response = await fetch('http://localhost:5001/api/Category/api/getcategories');

        return response.json();
    }

    async getCategoryItems(id, offset, limit) {
        const response = await fetch(`http://localhost:5001/api/Category/api/getcategoryitems?id=${id}&offset=${offset}&limit=${limit}`);
        
        return response.json();
    }
}

export const categoryService = new CategoryService();