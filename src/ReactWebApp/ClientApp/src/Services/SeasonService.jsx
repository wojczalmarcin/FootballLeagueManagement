import { HandleResponseError, HandleResponseErrorGet } from "./ResponseErrorHandling";

export const LoadSeasonsCurrentSeason = (setSeasons, setCurrentSeason) => {
    return (
        fetch('Api/Season')
            .then(HandleResponseErrorGet)
            .then((data) => {
                setSeasons(data);
                setCurrentSeason(data[0]);
            })
            .catch((error) => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const LoadSeasons = (setSeasons) => {
    return (
        fetch('Api/Season')
            .then(HandleResponseErrorGet)
            .then((data) => setSeasons(data))
            .catch((error) => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const EditSeason = (inputs) => {
    return (
        fetch('api/Season', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(inputs)
        })
            .then(HandleResponseError)
            .catch(error => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const AddSeason = (inputs) => {
    return (
        fetch('api/Season', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(inputs)
        })
            .then(HandleResponseError)
            .catch(error => {
                alert(error);
                console.error('Error:', error);
            })
    );
}