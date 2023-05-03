import { json } from "react-router-dom";

class UserService {
    async getUser(login) {
        const response = await fetch(`http://localhost:5001/api/getuserbylogin?login=${login}`);
        return response.json();
    }
}

export const userService = new UserService();