import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

const AIConsult = () => {
    const location = useLocation();
    const navigate = useNavigate();
    const [results, setResults] = useState({
        burnDepth: 'Hesaplanıyor...',
        burnPercentage: 'Hesaplanıyor...',
    });
    const patient = location.state?.patient;

    useEffect(() => {
        const fetchAiResults = async (patient) => {
            try {
                const response = await fetch('http://localhost:8000/yanik-analizi', { // Your Python API endpoint
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        burnArea: patient?.burnArea,
                        age: patient?.age,
                        gender: patient?.gender,
                        medicalHistory: patient?.medicalHistory,
                        burnCause: patient?.burnCause
                        // Diğer gerekli hasta verileri
                    }),
                });

                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }

                const data = await response.json();
                return data;
            } catch (error) {
                console.error("AI sonuçları alınırken hata:", error);
                return null; // Veya bir hata nesnesi döndürebilirsiniz
            }
        };

        const getAiResults = async () => {
            if (patient) {
                const aiResults = await fetchAiResults(patient);

                if (aiResults) {
                    setResults({
                        burnDepth: aiResults.burnDepth || 'Belirsiz',
                        burnPercentage: aiResults.burnPercentage || 'Belirsiz',
                    });
                } else {
                    // Hata durumunda varsayılan değerleri ayarlayın veya bir hata mesajı gösterin
                    setResults({
                        burnDepth: 'Hata oluştu',
                        burnPercentage: 'Hata oluştu',
                    });
                }
            }
        };

        getAiResults();
    }, [patient]);

    const handleBack = () => {
        navigate(-1, {state: { patient: patient }});  // Go back to the previous page
    };

    return (
        <div style={styles.container}>
            <div style={styles.header}>
                <span style={styles.backArrow} onClick={handleBack}>
                    ← Geri
                </span>
            </div>
            <h1>Yapay Zeka Konsültasyonu</h1>

            <div style={styles.resultsContainer}>
                <h2>Sonuçlar</h2>
                <p><strong>Yanık Derinliği:</strong> {results.burnDepth}</p>
                <p><strong>Yanık Yüzdesi (Tahmini):</strong> {results.burnPercentage}</p>
            </div>
        </div>
    );
};

const styles = {
    container: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        padding: '20px',
    },
    header: {
        width: "100%",
        display: "flex",
        justifyContent: "flex-start",
        padding: "10px 20px",
        position: "absolute",
        top: "10px",
        left: "10px",
    },
    backArrow: {
        padding: "10px 20px",
        backgroundColor: "#007bff",
        color: "white",
        border: "none",
        borderRadius: "5px",
        cursor: "pointer",
        fontWeight: "bold",
    },
    resultsContainer: {
        marginTop: '20px',
        padding: '15px',
        border: '1px solid #ccc',
        borderRadius: '8px',
        backgroundColor: '#f9f9f9',
        textAlign: 'left',
    },
};

export default AIConsult;