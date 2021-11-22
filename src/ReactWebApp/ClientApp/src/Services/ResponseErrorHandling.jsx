
export async function HandleResponseErrorGet (response) {
    if (response.status >= 400 && response.status !== 404) {
        await handleResponse(response);
    }
    return response.json();
}

export async function HandleResponseError (response) {
    console.log(response);
    if (!response.ok) {
        await handleResponse(response);
    }
    return response.json();
}

async function handleResponse (response) {
    var errorMessage = "";
    try {
        var error = await response.json()

        if (Array.isArray(error)) {
            error.forEach(e => {
                errorMessage += e + "\n";
            });

        }
        else {
            errorMessage = error;
        }
    }
    catch {
        errorMessage = response.status;
        console.log(errorMessage);
    }
    throw Error(errorMessage);
}