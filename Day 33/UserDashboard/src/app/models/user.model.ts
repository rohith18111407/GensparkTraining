export interface User
{
    id: number;
    firstName: string;
    lastName: string;
    age: number;
    gender: 'male' | 'female';
    company_title_role: string;
    state: string;
}