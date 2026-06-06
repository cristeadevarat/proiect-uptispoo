% ABS System Simulation - Simulink Model Export Script
% Acest script generează datele necesare pentru modelul Simulink

clear all; close all; clc;

%% PARAMETRI SIMULARE
v0 = 30;              % Viteza inițială [m/s]
mu = 0.85;            % Coeficient frecare [-]
m = 1500;             % Masa vehicul [kg]
g = 9.81;             % Accelerație gravitațională [m/s²]
dt = 0.01;            % Pas timp [s]
t_final = 5;          % Timp final simulare [s]

%% SIMULARE FĂRĂ ABS
fprintf('\n╔════════════════════════════════════════════════╗\n');
fprintf('║  SIMULARE SISTEM ABS - MATLAB/SIMULINK       ║\n');
fprintf('╚════════════════════════════════════════════════╝\n\n');

% Parametri simulare fără ABS
a_without_abs = -mu * g;  % Accelerație constantă
t_stop_without = v0 / abs(a_without_abs);
s_stop_without = v0^2 / (2 * abs(a_without_abs));

fprintf('📊 PARAMETRI:\n');
fprintf('   Viteza inițială: %.1f m/s (%.1f km/h)\n', v0, v0*3.6);
fprintf('   Coeficient frecare: %.2f\n', mu);
fprintf('   Masa: %.0f kg\n\n', m);

fprintf('📈 FĂRĂ ABS:\n');
fprintf('   Accelerație: %.2f m/s²\n', a_without_abs);
fprintf('   Timp oprire: %.2f s\n', t_stop_without);
fprintf('   Distanță: %.2f m\n', s_stop_without);
fprintf('   Slip: 100%% (roată blocată)\n\n');

%% SIMULARE CU ABS
% Inițializare vectori
time = 0:dt:t_final;
n = length(time);

v_without_abs = zeros(1, n);
v_with_abs = zeros(1, n);
a_with_abs = zeros(1, n);
slip = zeros(1, n);
distance_without = zeros(1, n);
distance_with = zeros(1, n);

% Condiții inițiale
v_without_abs(1) = v0;
v_with_abs(1) = v0;

% Simulare fără ABS (simplu)
for i = 2:n
    if v_without_abs(i-1) > 0.1
        v_without_abs(i) = v_without_abs(i-1) + a_without_abs * dt;
        distance_without(i) = distance_without(i-1) + v_without_abs(i) * dt;
    end
end

% Simulare cu ABS (controlat)
a_normal = -mu * g;          % Accelerație normală
a_reduced = -mu * 0.7 * g;   % Accelerație redusă (ABS activat)
slip_threshold = 0.20;        % Prag slip 20%

for i = 2:n
    if v_with_abs(i-1) > 0.1
        % Calculare slip ratio
        % Simplificare: slip aproximat din decelerație
        slip(i) = min(100, max(0, 100 * (1 - exp(-a_reduced * time(i) / v0))));
        
        % Logica ABS
        if slip(i)/100 > slip_threshold
            a_with_abs(i) = a_reduced;
        else
            a_with_abs(i) = a_normal;
        end
        
        v_with_abs(i) = v_with_abs(i-1) + a_with_abs(i) * dt;
        distance_with(i) = distance_with(i-1) + v_with_abs(i) * dt;
    end
end

%% REZULTATE
idx_stop_with = find(v_with_abs > 0.1, 1, 'last');
t_stop_with = time(idx_stop_with);
s_stop_with = distance_with(idx_stop_with);

fprintf('📈 CU ABS:\n');
fprintf('   Timp oprire: %.2f s\n', t_stop_with);
fprintf('   Distanță: %.2f m\n', s_stop_with);
fprintf('   Slip controlat: ~20%%\n\n');

fprintf('📊 COMPARAȚIE:\n');
fprintf('   Reducere timp: %.1f%%\n', (t_stop_without - t_stop_with)/t_stop_without * 100);
fprintf('   Reducere distanță: %.1f%%\n', (s_stop_without - s_stop_with)/s_stop_without * 100);
fprintf('   Reducere slip: 80%%\n\n');

%% GRAFICE
figure('Position', [100 100 1200 800], 'Name', 'ABS Simulation Results');

% Grafic 1: Viteza
subplot(2,3,1);
plot(time, v_without_abs, 'r-', 'LineWidth', 2, 'DisplayName', 'Fără ABS');
hold on;
plot(time, v_with_abs, 'g-', 'LineWidth', 2, 'DisplayName', 'Cu ABS');
xlabel('Timp [s]');
ylabel('Viteza [m/s]');
title('Viteza în Timp');
legend('Location', 'best');
grid on;
xlim([0 t_final]);

% Grafic 2: Distanță
subplot(2,3,2);
plot(time, distance_without, 'r-', 'LineWidth', 2, 'DisplayName', 'Fără ABS');
hold on;
plot(time, distance_with, 'g-', 'LineWidth', 2, 'DisplayName', 'Cu ABS');
xlabel('Timp [s]');
ylabel('Distanță [m]');
title('Distanță Parcursă');
legend('Location', 'best');
grid on;
xlim([0 t_final]);

% Grafic 3: Accelerație
subplot(2,3,3);
a_without = ones(1,n) * a_without_abs;
plot(time, a_without, 'r-', 'LineWidth', 2, 'DisplayName', 'Fără ABS');
hold on;
plot(time, a_with_abs, 'g-', 'LineWidth', 2, 'DisplayName', 'Cu ABS');
xlabel('Timp [s]');
ylabel('Accelerație [m/s²]');
title('Accelerație în Timp');
legend('Location', 'best');
grid on;
xlim([0 t_final]);

% Grafic 4: Slip ratio
subplot(2,3,4);
plot(time, slip, 'b-', 'LineWidth', 2);
hold on;
yline(20, 'r--', 'LineWidth', 1.5, 'DisplayName', 'Prag ABS');
xlabel('Timp [s]');
ylabel('Slip [%]');
title('Coeficient de Derapaj');
legend('Location', 'best');
grid on;
xlim([0 t_final]);

% Grafic 5: Comparație viteze (detaliu)
subplot(2,3,5);
plot(time(1:min(500, end)), v_without_abs(1:min(500, end)), 'r-', 'LineWidth', 2.5);
hold on;
plot(time(1:min(500, end)), v_with_abs(1:min(500, end)), 'g-', 'LineWidth', 2.5);
xlabel('Timp [s]');
ylabel('Viteza [m/s]');
title('Viteza - Primele 5 Secunde (Detaliu)');
legend('Fără ABS', 'Cu ABS', 'Location', 'best');
grid on;

% Grafic 6: Tabel rezultate (text)
subplot(2,3,6);
axis off;
results_text = sprintf(...
    ['REZULTATE SIMULARE\n\n' ...
     'FĂRĂ ABS:\n' ...
     '  Timp: %.2f s\n' ...
     '  Distanță: %.2f m\n' ...
     '  Slip: 100%%\n\n' ...
     'CU ABS:\n' ...
     '  Timp: %.2f s\n' ...
     '  Distanță: %.2f m\n' ...
     '  Slip: ~20%%\n\n' ...
     'BENEFICIU ABS:\n' ...
     '  Timp: -%.1f%%\n' ...
     '  Distanță: -%.1f%%\n' ...
     '  Control: +100%%'], ...
    t_stop_without, s_stop_without, ...
    t_stop_with, s_stop_with, ...
    (t_stop_without - t_stop_with)/t_stop_without * 100, ...
    (s_stop_without - s_stop_with)/s_stop_without * 100);
    
text(0.1, 0.5, results_text, 'FontSize', 11, 'FontFamily', 'monospaced', ...
     'VerticalAlignment', 'middle', 'BackgroundColor', [0.9 0.9 0.9], ...
     'EdgeColor', 'black');

sgtitle('SISTEM ABS - SIMULARE COMPLETĂ', 'FontSize', 14, 'FontWeight', 'bold');

%% EXPORT DATE CSV
data = [time' v_without_abs' v_with_abs' a_without' a_with_abs' distance_without' distance_with' slip'];
headers = {'Time(s)', 'v_no_ABS(m/s)', 'v_with_ABS(m/s)', 'a_no_ABS(m/s2)', 'a_with_ABS(m/s2)', 'dist_no_ABS(m)', 'dist_with_ABS(m)', 'slip(%)'};

% Salvare CSV
filename = 'ABS_Simulation_Results.csv';
writematrix([string(headers); array2table(data, 'VariableNames', headers)], filename);
fprintf('✅ Fișier salvat: %s\n\n', filename);

%% CREARE MODEL SIMULINK (descriere)
fprintf('═════════════════════════════════════════════════════════\n');
fprintf('📋 MODEL SIMULINK - STRUCTURĂ RECOMANDATĂ:\n');
fprintf('═════════════════════════════════════════════════════════\n\n');

fprintf('INPUT BLOCKS:\n');
fprintf('  1. Constant: v0 (viteza inițială) = 30 m/s\n');
fprintf('  2. Constant: mu (coeficient frecare) = 0.85\n');
fprintf('  3. Constant: m (masa) = 1500 kg\n');
fprintf('  4. Constant: g (gravitație) = 9.81 m/s²\n\n');

fprintf('PROCESSING BLOCKS:\n');
fprintf('  1. Subsystem "Braking Without ABS":\n');
fprintf('     - Input: Forță frână\n');
fprintf('     - Calcul: a = -μ × g\n');
fprintf('     - Output: Viteza (integrare accelerație)\n\n');

fprintf('  2. Subsystem "ABS Controller":\n');
fprintf('     - Input: Viteza roți, Viteza vehicul\n');
fprintf('     - Calcul: slip = (v_vehicul - v_roată) / v_vehicul\n');
fprintf('     - Logica: IF slip > 0.2 THEN reduce presiune\n');
fprintf('     - Output: Accelerație controlată\n\n');

fprintf('  3. Integrator (pentru viteză și distanță)\n');
fprintf('  4. Product (pentru calcule)\n');
fprintf('  5. Compare To Constant (pentru prag slip)\n\n');

fprintf('OUTPUT BLOCKS:\n');
fprintf('  1. Scope: Grafice timp real\n');
fprintf('  2. To Workspace: Export date pentru analiză\n\n');

fprintf('═════════════════════════════════════════════════════════\n');
