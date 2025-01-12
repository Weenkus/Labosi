#include <fstream>
#include <iostream>
#include <string>
#include <vector>

std::vector<char> complementSequence(std::string strand);

int main() {

	// Load the input file
	std::ifstream fileHandle("dataset_3_2.txt");
	std::string input;
	std::getline(fileHandle, input);

	// Generate and write the complementary strand
	std::vector<char> complementSeq = complementSequence(input);
	std::ofstream outputHandle("output.txt");

	for (const auto& c : complementSeq) {
		outputHandle << c;
	}

	outputHandle.close();
	fileHandle.close();
	return 0;
}

std::vector<char> complementSequence(std::string strand) {
	std::vector<char> complementStrand;
	for (const auto& c : strand) {
		char complement;
		switch (c) {
			case 'A':
				complement = 'T';
				break;
			case 'C':
				complement = 'G';
				break;
			case 'T':
				complement = 'A';
				break;
			case 'G':
				complement = 'C';
				break;
		}
		complementStrand.insert(complementStrand.begin(), complement);
	}
	return complementStrand;
}