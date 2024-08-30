import { addToWatchLater } from '../services/Api.js';
import {toast} from 'sonner'
import '../styles/toasts/toasts.css'

export const handleWatchLater = async (id) => {
    const movieIds = [id];
    try {
        await addToWatchLater(movieIds);
        toast.success('Добавлено в "Посмотреть позже"', {
            className: 'successToast',
            duration: 2000
        })
    } catch (err) {
        console.error(err);
        toast.error("Невозможно добавить фильм", {
            className: 'errorToast',
            duration: 2000
        });
    }
};