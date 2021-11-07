import { HandleResponseError } from "./ResponseErrorHandling";

export const LoadSeasonsCurrentSeason = (setSeasons, setCurrentSeason) => {
    return (
        fetch('Api/Season')
            .then(response => response.json())
            .then((data) => {
                if (data.responseStatus !== 200) {
                    HandleResponseError(data);
                }
                else {
                    setSeasons(data.data);
                    setCurrentSeason(data.data[0]);
                }
            })
            .catch((error) => {
                console.error('Error:', error);
            })
    );
}

export const LoadSeasons = (setSeasons) => {
    return (
        fetch('Api/Season')
            .then(response => response.json())
            .then((data) => {
                if (data.responseStatus !== 200) {
                    HandleResponseError(data);
                }
                else {
                    setSeasons(data.data);
                }
            })
            .catch((error) => {
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
            .then(response => response.json())
            .then(data => {
                if (data.responseStatus !== 200) {
                    HandleResponseError(data);
                }
            })
            .catch(error => {
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
            .then(response => response.json())
            .then(data => {
                if (data.responseStatus !== 200) {
                    HandleResponseError(data);
                }
            })
            .catch(error => {
                console.error('Error:', error);
            })
    );
}