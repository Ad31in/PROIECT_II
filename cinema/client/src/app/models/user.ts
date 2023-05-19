export interface User {
    id: string;
    firstName: string;
    email: string;
    token?: string;
    state?: string;
}