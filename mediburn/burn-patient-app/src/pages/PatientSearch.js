import React, { useEffect, useState } from "react";
import { FaChevronLeft, FaChevronRight } from "react-icons/fa"; // Importing small arrow icons
import { useNavigate } from "react-router-dom"; // For navigation
import "./PatientSearch.css";

const PatientSearch = () => {
  const [patients, setPatients] = useState([]);
  const [filteredPatients, setFilteredPatients] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [searchTerm, setSearchTerm] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const [recordsPerPage, setRecordsPerPage] = useState(10); // Number of records per page
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPatients = async () => {
      setLoading(true);
      setError(null);

      try {
        const response = await fetch("http://localhost:5005/api/Patient");

        if (!response.ok) {
          throw new Error("Hastalar alınırken bir hata oluştu.");
        }

        const data = await response.json();
        setPatients(data);
        setFilteredPatients(data); // Initially, no filter
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    fetchPatients();
  }, []);

  // Handle search input change
  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
  };

  // Filter patients by name
  useEffect(() => {
    const filtered = patients.filter((patient) =>
      patient.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setFilteredPatients(filtered);
    setCurrentPage(1); // Reset to first page when search term changes
  }, [searchTerm, patients]);

  // Handle pagination logic
  const indexOfLastPatient = currentPage * recordsPerPage;
  const indexOfFirstPatient = indexOfLastPatient - recordsPerPage;
  const currentPatients = filteredPatients.slice(indexOfFirstPatient, indexOfLastPatient);

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  // Edit and View functions for navigation
  const handleEdit = (patientId) => {
    console.log("Navigating to edit page for patient:", patientId); // Test console log
    navigate(`/edit-patient/${patientId}`);
  };

  const handleView = (patientId) => {
    console.log("Navigating to view page for patient:", patientId); // Test console log
    navigate(`/view-patient/${patientId}`);
  };

  const handleDelete = async (patientId) => {
    if (window.confirm("Bu hastayı silmek istediğinizden emin misiniz?")) {
      try {
        const response = await fetch(
          `http://localhost:5005/api/Patient/${patientId}`,
          {
            method: "DELETE",
          }
        );

        if (!response.ok) {
          throw new Error("Hastayı silerken bir hata oluştu.");
        }

        alert("Hasta başarıyla silindi");
        setPatients(patients.filter((patient) => patient.patientID !== patientId));
      } catch (error) {
        alert(error.message);
      }
    }
  };

  // Pagination controls
  const pageNumbers = [];
  for (let i = 1; i <= Math.ceil(filteredPatients.length / recordsPerPage); i++) {
    pageNumbers.push(i);
  }

  // Handle next and previous page logic
  const handleNextPage = () => {
    if (currentPage < pageNumbers.length) {
      setCurrentPage(currentPage + 1);
    }
  };

  const handlePrevPage = () => {
    if (currentPage > 1) {
      setCurrentPage(currentPage - 1);
    }
  };

  return (
    <div>
      <h2>Hasta Kayıtları</h2>

      {loading && <p>Yükleniyor...</p>}
      {error && <p style={{ color: "red" }}>{error}</p>}

      <div className="search-container">
        <input
          type="text"
          placeholder="Hasta adına göre ara"
          value={searchTerm}
          onChange={handleSearchChange}
        />
      </div>

      <table>
        <thead>
          <tr>
            <th>Adı</th>
            <th>Yaş</th>
            <th>Cinsiyet</th>
            <th>Tıbbi Geçmiş</th>
            <th>İşlemler</th>
          </tr>
        </thead>
        <tbody>
          {currentPatients.length > 0 ? (
            currentPatients.map((patient) => (
              <tr key={patient.patientID}>
                <td>{patient.name}</td>
                <td>{patient.age}</td>
                <td>{patient.gender}</td>
                <td>{patient.medicalHistory}</td>
                <td>
                  <div className="button-group">
                    <button onClick={() => handleView(patient.patientID)}>Görüntüle</button>
                    <button onClick={() => handleEdit(patient.patientID)}>Düzenle</button>
                    <button onClick={() => handleDelete(patient.patientID)}>Sil</button>
                  </div>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="5">Hasta bulunamadı.</td>
            </tr>
          )}
        </tbody>
      </table>

      {/* Pagination controls with small arrows */}
      <div className="pagination">
        <span>Sayfa Başına Kayıt Sayısı:</span>
        <select
          value={recordsPerPage}
          onChange={(e) => setRecordsPerPage(parseInt(e.target.value))}
        >
          <option value={5}>5</option>
          <option value={10}>10</option>
          <option value={15}>15</option>
          <option value={20}>20</option>
        </select>

        <div className="page-navigation">
          <button
            onClick={handlePrevPage}
            disabled={currentPage === 1}
            className="arrow-button"
          >
            <FaChevronLeft /> {/* Left Arrow Icon */}
          </button>
          <span>Sayfa {currentPage} / {pageNumbers.length}</span>
          <button
            onClick={handleNextPage}
            disabled={currentPage === pageNumbers.length}
            className="arrow-button"
          >
            <FaChevronRight /> {/* Right Arrow Icon */}
          </button>
        </div>
      </div>
    </div>
  );
};

export default PatientSearch;
